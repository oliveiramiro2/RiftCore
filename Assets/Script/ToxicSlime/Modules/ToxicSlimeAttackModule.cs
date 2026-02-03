using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSlimeAttackModule : MonoBehaviour
{
  private ToxicSlimeController owner;
  private Transform player;
  public bool isAttacking = false;
  [SerializeField] private float attackCooldown = 3f;


  [Header("Decision Timers")]
  [SerializeField] private float minDecisionTime = 2f;
  [SerializeField] private float maxDecisionTime = 4f;
  [SerializeField] private float minDecisionTimePhase2 = 1f;
  [SerializeField] private float maxDecisionTimePhase2 = 3f;

  [Header("Prefabs")]
  public GameObject rainDropPrefab;
  public GameObject projectilePrefab;

  [Header("Projectile Settings")]
  public float spawnHeight = 3f;
  public float travelTime = 2f;
  public float arcHeight = 2.5f;

  public bool canAttackTimer = true;
  private float attackTimer = 0f;


  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {
    if (owner.IsDead) return;

    if (!canAttackTimer) return;
    isAttacking = false;
    attackTimer += Time.deltaTime;
    if (attackTimer >= attackCooldown)
    {
      isAttacking = true;
      attackTimer = 0f;
      ResetTimer();
    }
  }

  public void Initialize(ToxicSlimeController controller)
  {
    owner = controller;
  }

  public void ResetTimer()
  {
    if (owner.Phase2())
    {
      attackCooldown = Random.Range(minDecisionTimePhase2, maxDecisionTimePhase2);
    }
    else
    {
      attackCooldown = Random.Range(minDecisionTime, maxDecisionTime);
    }
  }

  public void DecideNextAttack(ToxicSlimeController entity)
  {
    float dist = Vector2.Distance(transform.position, player.position);

    List<System.Action> validAttacks = new()
    {
      () => JumpRoll(entity), // rolling jump towards player
      () => RollAttack(entity), // rolling attack and jump at the end
    };

    if (dist < 3f)
    {
      validAttacks.Add(() => SlapAttack(entity)); // slap attack
    }

    if (entity.Phase2())
    {
      validAttacks.Add(() => ToxicRainAttack(entity));
      validAttacks.Add(() => SpalshAttack(entity));
    }

    int index = Random.Range(0, validAttacks.Count);

    if (owner.IsDead) return;

    validAttacks[index].Invoke();
  }

  private void JumpRoll(ToxicSlimeController entity)
  {
    float jumpHeight = 5f;
    float jumpDuration = 0.7f;
    float rollDuration = 0.7f;
    float rotations = 4f;


    StartCoroutine(RollJumpRoutine(entity, rollDuration, rotations, jumpHeight, jumpDuration));
  }

  private IEnumerator RollJumpRoutine(ToxicSlimeController entity, float rollDuration, float rotations, float jumpHeight, float jumpDuration)
  {
    entity.AnimatorBridge.ToxicSlimeBallStart();

    yield return new WaitForSeconds(0.5f);

    entity.Locomotion.Roll(rollDuration, rotations);
    entity.Locomotion.JumpAtTarget(jumpHeight, jumpDuration);

    yield return new WaitForSeconds(rollDuration);

    RumbleManager.Instance.Play(RumbleType.HeavyHit);
    entity.AnimatorBridge.ToxicSlimeBallEnd();

    yield return new WaitForSeconds(0.6f);

    entity.isAttacking = false;
  }

  private void RollAttack(ToxicSlimeController entity)
  {
    float rollDuration = 1.5f;
    float rotations = 16f;
    StartCoroutine(RollAttackRoutine(entity, rollDuration, rotations));
  }

  private IEnumerator RollAttackRoutine(ToxicSlimeController entity, float rollDuration, float rotations)
  {
    float jumpRollDuration = 1f, jumpRotation = 8f, jumpHeight = 5f, jumpDuration = 1f;


    entity.Locomotion.FlipTowardsTarget(entity);
    entity.AnimatorBridge.ToxicSlimeBallStart();

    yield return new WaitForSeconds(0.5f);

    entity.Locomotion.Rolling(entity);
    entity.Locomotion.Roll(rollDuration, rotations, false);

    yield return new WaitForSeconds(rollDuration);

    RumbleManager.Instance.Play(RumbleType.Danger);
    StartCoroutine(RollJumpRoutine(entity, jumpRollDuration, jumpRotation, jumpHeight, jumpDuration));
  }

  private void SlapAttack(ToxicSlimeController entity)
  {
    entity.AnimatorBridge.ToxicSlimeSlap();

    StartCoroutine(SlapAttackRoutine(entity));
  }

  private IEnumerator SlapAttackRoutine(ToxicSlimeController entity)
  {
    yield return new WaitForSeconds(1.2f);
    entity.isAttacking = false;
  }

  private void ToxicRainAttack(ToxicSlimeController entity)
  {
    entity.AnimatorBridge.ToxicSlimeToxicRain();

    StartCoroutine(ToxicRainAttackRoutine(entity));
  }

  private IEnumerator ToxicRainAttackRoutine(ToxicSlimeController entity)
  {
    yield return new WaitForSeconds(0.5f);
    entity.tsEvents.OnToxicRainStart.Raise();
    yield return new WaitForSeconds(1.5f);
    entity.AnimatorBridge.ToxicSlimeToxicRainEnd();
    entity.tsEvents.OnToxicRainCloundAppear.Raise();
    List<Vector2> points = owner.Physics.GetRandomPoints();

    foreach (Vector2 pos in points)
    {
      Instantiate(rainDropPrefab, pos + Vector2.up * 6f, Quaternion.identity);
      yield return new WaitForSeconds(0.2f);
    }
    entity.isAttacking = false;
  }

  private void SpalshAttack(ToxicSlimeController entity)
  {
    entity.AnimatorBridge.ToxicSlimeSplash();

    StartCoroutine(SplashAttackRoutine(entity));
  }

  private IEnumerator SplashAttackRoutine(ToxicSlimeController entity)
  {
    yield return new WaitForSeconds(0.55f);

    List<Vector2> points = entity.Physics.GetRandomPoints();

    foreach (Vector2 target in points)
    {
      SpawnProjectile(entity, target);
      yield return new WaitForSeconds(0.05f);
    }

    yield return new WaitForSeconds(1f);

    entity.isAttacking = false;
  }

  void SpawnProjectile(ToxicSlimeController entity, Vector2 target)
  {
    Vector2 spawnPos = (Vector2)entity.transform.position;

    var gameObjectAux = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

    var proj = gameObjectAux.GetComponent<ToxicSplashProjectile>();
    proj.Launch(target, travelTime, arcHeight);
  }
}
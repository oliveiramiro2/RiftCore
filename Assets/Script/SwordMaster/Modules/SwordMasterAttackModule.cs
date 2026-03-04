using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SwordMasterAttackModule : MonoBehaviour
{
  private SwordMasterController owner;
  private Transform player;

  public bool isAttacking = false;
  [SerializeField] private float attackCooldown = 3f;
  public bool canAttackTimer = true;
  private float attackTimer = 0f;

  [Header("Attack Prefabs")]
  [SerializeField] private GameObject stormPrefab;
  [SerializeField] private GameObject[] airSlashPrefab;
  [SerializeField] private Transform stormRespawnPoint;
  [SerializeField] private Transform[] airSlashsRespawnPoint;

  [Header("Decision Timers")]
  private readonly float minDecisionTime = 3f;
  private readonly float maxDecisionTime = 4f;
  private readonly float minDecisionTimePhase2 = 1f;
  private readonly float maxDecisionTimePhase2 = 2f;

  void Start()
  {
    player = GameObject.FindAnyObjectByType<PlayerController>().transform;
  }

  public void Initialize(SwordMasterController controller)
  {
    owner = controller;
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

  public void DecideNextAttack(SwordMasterController entity)
  {
    float dist = Vector2.Distance(transform.position, player.position);

    List<System.Action> validAttacks = new()
    {
      () => TreeSlashAttack(entity),
      () => ExplosionAttack(entity),
      () => StormAttack(entity),
      () => Parry(entity),
      () => WindSlash(entity)
    };

    // if (entity.Phase2())
    // {

    // }

    int index = Random.Range(0, validAttacks.Count);

    if (owner.IsDead) return;
    owner.LocomotionModule.StopMovement();
    validAttacks[index].Invoke();
  }

  private void TreeSlashAttack(SwordMasterController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(player);
    entity.AnimatorBridge.SwordMasterFirstAttack();
    StartCoroutine(TeleportRoutine(entity));
  }

  private void ExplosionAttack(SwordMasterController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(player);
    entity.AnimatorBridge.SwordMasterExplosion();
    StartCoroutine(ExplosionRoutine(entity));
  }

  private void StormAttack(SwordMasterController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(player);
    entity.AnimatorBridge.SwordMasterStorm();
    StartCoroutine(StormRoutine(entity));
  }

  private void Parry(SwordMasterController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(player);
    entity.AnimatorBridge.SwordMasterParry();
    StartCoroutine(WaitAnimationFinish(entity));
  }

  private void WindSlash(SwordMasterController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(player);
    entity.AnimatorBridge.SwordMasterWindSlash();
    StartCoroutine(AirSlashRoutine(entity));
  }

  private IEnumerator WaitAnimationFinish(SwordMasterController entity)
  {
    yield return new WaitForSeconds(1.5f);
    entity.isAttacking = false;
  }

  private IEnumerator ExplosionRoutine(SwordMasterController entity)
  {
    yield return new WaitForSeconds(2.5f);
    entity.isAttacking = false;
  }

  private IEnumerator TeleportRoutine(SwordMasterController entity)
  {
    while (!entity.canTeleport)
    {
      yield return null;
    }
    entity.canTeleport = false;

    entity.spriteRenderer.enabled = false;
    entity.rb.linearVelocity = Vector2.zero;

    yield return new WaitForSeconds(0.15f);
    float direction = player.position.x > entity.rb.position.x ? 1f : -1f;

    float distanceBehind = 1.5f;

    Vector2 newPosition = new Vector2(
        player.position.x - direction * distanceBehind,
        entity.rb.position.y
    );
    entity.rb.position = newPosition;
    entity.LocomotionModule.FlipTowardsTarget(player);
    RumbleManager.Instance.Play(RumbleType.Charge);
    yield return new WaitForSeconds(0.05f);

    entity.spriteRenderer.enabled = true;

    RumbleManager.Instance.Play(RumbleType.Danger);
    entity.AnimatorBridge.SwordMasterTripleAttack();
    yield return new WaitForSeconds(1.5f);
    entity.isAttacking = false;
  }

  private IEnumerator AirSlashRoutine(SwordMasterController entity)
  {
    yield return new WaitForSeconds(0.9f);

    int aux = Random.Range(0, airSlashPrefab.Length);
    airSlashPrefab[aux].transform.position = airSlashsRespawnPoint[aux].position;
    airSlashPrefab[aux].SetActive(true);
    yield return new WaitForSeconds(0.3f);

    int aux2 = Random.Range(0, airSlashPrefab.Length);
    if (aux2 == aux)
    {
      aux2 = (aux2 + 1) % airSlashPrefab.Length;
    }

    airSlashPrefab[aux2].transform.position = airSlashsRespawnPoint[aux2].position;
    airSlashPrefab[aux2].SetActive(true);

    yield return new WaitForSeconds(0.5f);
    entity.isAttacking = false;
  }

  private IEnumerator StormRoutine(SwordMasterController entity)
  {
    yield return new WaitForSeconds(1.5f);

    stormPrefab.transform.position = stormRespawnPoint.position;
    stormPrefab.SetActive(true);

    yield return new WaitForSeconds(0.1f);
    entity.isAttacking = false;
  }
}
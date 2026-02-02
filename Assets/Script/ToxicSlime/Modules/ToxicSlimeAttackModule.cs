using System.Collections;
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

    System.Collections.Generic.List<System.Action> validAttacks = new()
    {
      //() => JumpRoll(entity), // rolling jump towards player
      //() => RollAttack(entity), // rolling attack and jump at the end
      () => ToxicRainAttack(entity)
    };

    if (dist < 3f)
    {
      validAttacks.Add(() => SlapAttack(entity)); // slap attack
    }

    if (entity.Phase2())
    {
      validAttacks.Add(() => ToxicRainAttack(entity));
      validAttacks.Add(() => Attack5(entity));
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
    yield return new WaitForSeconds(1f);
    entity.isAttacking = false;
  }

  private void Attack5(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 5");
  }
}
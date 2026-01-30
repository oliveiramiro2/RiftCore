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
    Debug.Log("is dead: " + owner.IsDead);
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
      () => Attack1(entity),
      // () => Attack2(entity),
      // () => Attack3(entity)
    };

    if (entity.Phase2())
    {
      validAttacks.Add(() => Attack4(entity));
      validAttacks.Add(() => Attack5(entity));
    }

    int index = Random.Range(0, validAttacks.Count);

    if (owner.IsDead) return;

    validAttacks[index].Invoke();
  }

  private void Attack1(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 1");
    Vector2 targetPosition = player.position;
    float jumpHeight = 5f;
    float jumpDuration = 1f;
    float rollDuration = 1f;
    float rotations = 3f;


    StartCoroutine(RollJumpRoutine(entity, targetPosition, rollDuration, rotations, jumpHeight, jumpDuration));
  }

  private IEnumerator RollJumpRoutine(ToxicSlimeController entity, Vector2 targetPosition, float rollDuration, float rotations, float jumpHeight, float jumpDuration)
  {
    entity.AnimatorBridge.ToxicSlimeBallStart();
    yield return new WaitForSeconds(0.5f);
    entity.Locomotion.Roll(rollDuration, rotations);
    entity.Locomotion.JumpAtTarget(targetPosition, jumpHeight, jumpDuration);
    yield return new WaitForSeconds(rollDuration);
    entity.AnimatorBridge.ToxicSlimeBallEnd();
  }

  private void Attack2(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 2");
  }

  private void Attack3(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 3");
  }

  private void Attack4(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 4");
  }

  private void Attack5(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 5");
  }
}
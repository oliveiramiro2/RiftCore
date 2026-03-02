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
    entity.AnimatorBridge.SwordMasterFirstAttack();
    StartCoroutine(WaitAnimationFinish(entity));
  }

  private void ExplosionAttack(SwordMasterController entity)
  {
    entity.AnimatorBridge.SwordMasterExplosion();
    StartCoroutine(WaitAnimationFinish(entity));
  }

  private void StormAttack(SwordMasterController entity)
  {
    entity.AnimatorBridge.SwordMasterStorm();
    StartCoroutine(WaitAnimationFinish(entity));
  }

  private void Parry(SwordMasterController entity)
  {
    entity.AnimatorBridge.SwordMasterParry();
    StartCoroutine(WaitAnimationFinish(entity));
  }

  private void WindSlash(SwordMasterController entity)
  {
    entity.AnimatorBridge.SwordMasterWindSlash();
    StartCoroutine(WaitAnimationFinish(entity));
  }

  private IEnumerator WaitAnimationFinish(SwordMasterController entity)
  {
    yield return new WaitForSeconds(5f);
    entity.isAttacking = false;
  }
}
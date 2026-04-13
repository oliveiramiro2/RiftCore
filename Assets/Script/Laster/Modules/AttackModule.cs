using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LasterAttackModule : MonoBehaviour
{
  private LasterController owner;
  private Transform player;

  public bool isAttacking = false;
  public float attackCooldown = 3f;
  public bool canAttackTimer = true;
  private float attackTimer = 0f;

  [Header("Attack Prefabs")]
  [SerializeField] private GameObject shadowHandPrefab;
  [SerializeField] private GameObject handAttackPrefab;
  [SerializeField] private Transform shadowHandSpawnPoint;
  [SerializeField] private GameObject[] bonesPrefab;
  [SerializeField] private Transform[] boneSpawnPoint;
  [SerializeField] private GameObject zombie1, zombie2;
  [SerializeField] private Transform zombie1Pos, zombie2Pos;


  [Header("Decision Timers")]
  private readonly float minDecisionTime = 2f;
  private readonly float maxDecisionTime = 3f;
  private readonly float minDecisionTimePhase2 = 1f;
  private readonly float maxDecisionTimePhase2 = 2f;

  void Start()
  {
    player = GameObject.FindAnyObjectByType<PlayerController>().transform;
  }

  public void Setup(LasterController controller)
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

  public void DecideNextAttack(LasterController entity)
  {
    float dist = Vector2.Distance(transform.position, player.position);

    List<System.Action> validAttacks = new()
    {
      LaserAttack,
      SwordArenaAttack,
      SlashAttack,
      GreatBallAttack
    };

    if (entity.Phase2())
    {
      if (dist <= 1f)
      {

      }
    }

    int index = Random.Range(0, validAttacks.Count);

    if (owner.IsDead) return;

    owner.Locomotion.StopMovement();
    validAttacks[index].Invoke();
  }

  private void LaserAttack()
  {
    owner.AnimatorBridge.PlayLaserAttack();
    StartCoroutine(LaserAttackRoutine(owner));
  }

  private void SwordArenaAttack()
  {
    owner.AnimatorBridge.PlaySwordArenaAttack();
    StartCoroutine(SwordArenaAttackRoutine(owner));
  }

  private void SlashAttack()
  {
    owner.AnimatorBridge.PlaySlashAttack();
    StartCoroutine(SlashAttackRoutine(owner));
  }

  private void GreatBallAttack()
  {
    owner.AnimatorBridge.PlayGreatBallAttack();
    StartCoroutine(GreatBallAttackRoutine(owner));
  }



  private IEnumerator LaserAttackRoutine(LasterController entity)
  {
    yield return new WaitForSeconds(2.5f);
    entity.Locomotion.FlipTowardsTarget(player);
    entity.isAttacking = false;
  }



  private IEnumerator SwordArenaAttackRoutine(LasterController entity)
  {
    yield return new WaitForSeconds(1.2f);
    entity.Locomotion.FlipTowardsTarget(player);
    entity.isAttacking = false;
  }



  private IEnumerator SlashAttackRoutine(LasterController entity)
  {
    yield return new WaitForSeconds(0.5f);
    entity.Locomotion.FlipTowardsTarget(player);
    entity.isAttacking = false;
  }



  private IEnumerator GreatBallAttackRoutine(LasterController entity)
  {
    yield return new WaitForSeconds(1.2f);
    entity.Locomotion.FlipTowardsTarget(player);
    entity.isAttacking = false;
  }
}
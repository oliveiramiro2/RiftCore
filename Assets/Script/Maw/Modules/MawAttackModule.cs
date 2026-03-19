using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MawAttackModule : MonoBehaviour
{
  private MawController owner;
  private Transform player;

  public bool isAttacking = false;
  [SerializeField] private float attackCooldown = 3f;
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

  public void Setup(MawController controller)
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

  public void DecideNextAttack(MawController entity)
  {
    float dist = Vector2.Distance(transform.position, player.position);

    List<System.Action> validAttacks = new()
    {
      () => SummonAttack(entity),
      () => BoneAttack(entity),
      () => ExplosionAttack(entity),
      () => HandAttack(entity)
    };

    int index = Random.Range(0, validAttacks.Count);

    if (owner.IsDead) return;

    owner.Locomotion.StopMovement();
    validAttacks[index].Invoke();
  }

  private void SummonAttack(MawController entity)
  {
    StartCoroutine(SummonAttackRoutine(entity));
  }

  private IEnumerator SummonAttackRoutine(MawController entity)
  {
    if (!entity.hasStaffSummoned)
    {
      entity.AnimatorBridge.MawSummonStaff();
      yield return new WaitForSeconds(1.5f);
    }

    entity.AnimatorBridge.MawSummonAttack();
    yield return new WaitForSeconds(3f);

    zombie1.transform.position = new Vector3(zombie1Pos.transform.position.x, zombie1.transform.position.y, zombie1.transform.position.z);
    zombie2.transform.position = new Vector3(zombie2Pos.transform.position.x, zombie2.transform.position.y, zombie2.transform.position.z);

    zombie1.SetActive(true);
    zombie2.SetActive(true);

    yield return new WaitForSeconds(0.5f);

    float range = Random.Range(0f, 1f);
    if (range < 0.5f)
      entity.canFollowPlayer = true;
    else
    {
      entity.AnimatorBridge.MawHideStaff();
      yield return new WaitForSeconds(2f);
    }



    entity.isAttacking = false;
  }

  private void BoneAttack(MawController entity)
  {
    StartCoroutine(BoneAttackRoutine(entity));
  }

  private IEnumerator BoneAttackRoutine(MawController entity)
  {
    entity.AnimatorBridge.MawBoneAttack();
    yield return new WaitForSeconds(1.3f);

    entity.Locomotion.FlipTowardsTarget(player);

    yield return new WaitForSeconds(0.2f);

    bonesPrefab[0].transform.position = boneSpawnPoint[0].position;
    bonesPrefab[1].transform.position = boneSpawnPoint[1].position;

    bonesPrefab[0].SetActive(true);
    bonesPrefab[1].SetActive(true);

    yield return new WaitForSeconds(1f);

    float range = Random.Range(0f, 1f);
    if (range < 0.5f)
      entity.canFollowPlayer = true;
    entity.isAttacking = false;
  }

  private void ExplosionAttack(MawController entity)
  {
    StartCoroutine(ExplosionAttackRoutine(entity));
  }

  private IEnumerator ExplosionAttackRoutine(MawController entity)
  {
    if (!entity.hasStaffSummoned)
    {
      entity.AnimatorBridge.MawSummonStaff();
      yield return new WaitForSeconds(1.5f);
    }

    entity.AnimatorBridge.MawExplosion();
    yield return new WaitForSeconds(2.6f);

    float range = Random.Range(0f, 1f);
    if (range < 0.5f)
      entity.canFollowPlayer = true;
    else
    {
      entity.AnimatorBridge.MawHideStaff();
      yield return new WaitForSeconds(2f);
    }


    entity.isAttacking = false;
  }

  private void HandAttack(MawController entity)
  {
    StartCoroutine(HandAttackRoutine(entity));
  }

  private IEnumerator HandAttackRoutine(MawController entity)
  {
    if (!entity.hasStaffSummoned)
    {
      entity.AnimatorBridge.MawSummonStaff();
      yield return new WaitForSeconds(1.5f);
    }

    entity.AnimatorBridge.MawHandAttack();
    yield return new WaitForSeconds(2f);

    shadowHandPrefab.transform.position = shadowHandSpawnPoint.position;
    shadowHandPrefab.SetActive(true);

    yield return new WaitForSeconds(1f);
    handAttackPrefab.transform.position = shadowHandPrefab.transform.position;
    handAttackPrefab.SetActive(true);

    if (owner.Phase2())
    {
      yield return new WaitForSeconds(1.6f);
      handAttackPrefab.SetActive(false);

      yield return new WaitForSeconds(0.5f);
      handAttackPrefab.transform.position = shadowHandPrefab.transform.position;
      handAttackPrefab.SetActive(true);
    }
    shadowHandPrefab.SetActive(false);

    yield return new WaitForSeconds(1.6f);
    handAttackPrefab.SetActive(false);

    yield return new WaitForSeconds(0.5f);

    entity.AnimatorBridge.MawFinishHandAttack();
    yield return new WaitForSeconds(1f);

    float range = Random.Range(0f, 1f);
    if (range < 0.5f)
      entity.canFollowPlayer = true;
    else
    {
      entity.AnimatorBridge.MawHideStaff();
      yield return new WaitForSeconds(2f);
    }


    entity.isAttacking = false;
  }
}
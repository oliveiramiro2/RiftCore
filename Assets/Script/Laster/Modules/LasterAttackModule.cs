using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;

public class LasterAttackModule : MonoBehaviour
{
  private LasterController owner;
  private Transform player;
  public AttackSpawner attackSpawner;
  public GameObject[] prefabs;

  public bool isAttacking = false;
  public float attackCooldown = 3f;
  public bool canAttackTimer = true;


  [Header("Decision Timers")]
  private readonly float minDecisionTime = 2f;
  private readonly float maxDecisionTime = 3f;
  private readonly float minDecisionTimePhase2 = 1f;
  private readonly float maxDecisionTimePhase2 = 2f;

  [Header("Laser Attack")]
  public LasterLaser laser;
  private bool laserActive;


  [Header("Sword Arena Attack")]
  public Transform[] spawnerPoints;

  [Header("Great Ball Attack")]
  public Transform[] greatBallPoints;
  public float groundY = -5.59f;

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

    if (!canAttackTimer && isAttacking) return;
    if (laserActive)
    {
      laser.Fire(Vector2.right);
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
    owner.AnimatorBridge.PlayTeleportOut();
    owner.events.TeleportOut.Raise();
    StartCoroutine(TeleportRoutine(owner));
  }

  private void GreatBallAttack()
  {
    owner.AnimatorBridge.PlayGreatBallAttack();
    StartCoroutine(GreatBallAttackRoutine(owner));
  }



  private IEnumerator LaserAttackRoutine(LasterController entity)
  {
    entity.Locomotion.FlipToLaserPoint();
    yield return new WaitForSeconds(1.4f);
    laser.gameObject.SetActive(true);
    laserActive = true;

    yield return new WaitForSeconds(0.5f);
    laserActive = false;

    laser.gameObject.SetActive(false);
    float postAttackDelay = !entity.Phase2() ? 0.6f : 0f;
    yield return new WaitForSeconds(postAttackDelay);
    entity.Locomotion.FlipTowardsTarget(player);

    if (entity.Phase2())
    {
      owner.AnimatorBridge.LasterIdle();
      yield return new WaitForSeconds(0.1f);
      owner.AnimatorBridge.PlayLaserAttack();
      entity.Locomotion.FlipToLaserPoint();
      yield return new WaitForSeconds(1.4f);
      laser.gameObject.SetActive(true);
      laserActive = true;

      yield return new WaitForSeconds(0.5f);
      laserActive = false;

      laser.gameObject.SetActive(false);
      yield return new WaitForSeconds(0.6f);
    }

    entity.isAttacking = false;
  }



  private IEnumerator SwordArenaAttackRoutine(LasterController entity)
  {
    attackSpawner.ChangePrefab(prefabs[0]);
    yield return new WaitForSeconds(1.2f);
    entity.Locomotion.FlipTowardsTarget(player);

    int index = Random.Range(0, spawnerPoints.Length);
    attackSpawner.SpawnAttack(spawnerPoints[index].position, Quaternion.identity);

    int index2 = Random.Range(0, spawnerPoints.Length);
    while (index2 == index)
    {
      index2 = Random.Range(0, spawnerPoints.Length);
    }
    attackSpawner.SpawnAttack(spawnerPoints[index2].position, Quaternion.identity);

    if (entity.Phase2())
    {
      int index3 = Random.Range(0, spawnerPoints.Length);
      while (index3 == index || index3 == index2)
      {
        index3 = Random.Range(0, spawnerPoints.Length);
      }
      attackSpawner.SpawnAttack(spawnerPoints[index3].position, Quaternion.identity);
    }

    yield return new WaitForSeconds(0.5f);
    entity.isAttacking = false;
  }



  private IEnumerator TeleportRoutine(LasterController entity)
  {
    yield return new WaitForSeconds(Random.Range(2.5f, 4f));

    Vector3 auxPos = player.position + (Vector3)(Vector2.up * 1.5f);
    entity.transform.position = auxPos + (Vector3)(Vector2.left * 1.5f);
    entity.AnimatorBridge.PlayTeleportIn();
    entity.events.TeleportIn.Raise();
    yield return new WaitForSeconds(1f);
    entity.Locomotion.FlipTowardsTarget(player);
    entity.AnimatorBridge.PlaySlashAttack();
    yield return new WaitForSeconds(1f);
    entity.AnimatorBridge.PlayTeleportOut();
    entity.events.TeleportOut.Raise();
    yield return new WaitForSeconds(1f);
    entity.transform.position = Vector3.one;
    entity.AnimatorBridge.PlayTeleportIn();
    entity.events.TeleportIn.Raise();
    yield return new WaitForSeconds(1.5f);

    entity.isAttacking = false;

  }



  private IEnumerator GreatBallAttackRoutine(LasterController entity)
  {

    yield return new WaitForSeconds(1f);
    entity.Locomotion.FlipTowardsTarget(player);

    Vector3 aux = new Vector3(greatBallPoints[0].position.x, greatBallPoints[0].position.y, 0f);
    attackSpawner.ChangePrefab(prefabs[1]);
    attackSpawner.SpawnAttack(aux, Quaternion.identity);

    yield return new WaitForSeconds(0.5f);

    attackSpawner.ChangePrefab(prefabs[2]);
    aux = new Vector3(greatBallPoints[0].position.x, greatBallPoints[1].position.y, 0f);
    attackSpawner.SpawnAttack(aux, Quaternion.identity);

    yield return new WaitForSeconds(0.4f);

    if (entity.Phase2())
    {
      Vector3 position = new Vector3(greatBallPoints[0].position.x, greatBallPoints[1].position.y + 0.8f, 0f);

      attackSpawner.ChangePrefab(prefabs[3]);
      attackSpawner.SpawnAttack(position, Quaternion.identity);

      position = new Vector3(greatBallPoints[0].position.x - 1, greatBallPoints[1].position.y + 0.8f, 0f);
      attackSpawner.ChangePrefab(prefabs[4]);
      attackSpawner.SpawnAttack(position, Quaternion.identity);
    }
    yield return new WaitForSeconds(0.1f);
    entity.AnimatorBridge.LasterIdle();

    yield return new WaitForSeconds(2.5f);
    entity.Locomotion.FlipTowardsTarget(player);
    entity.isAttacking = false;
  }
}
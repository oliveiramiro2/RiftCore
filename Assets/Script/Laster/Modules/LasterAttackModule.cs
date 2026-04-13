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


  [Header("Decision Timers")]
  private readonly float minDecisionTime = 2f;
  private readonly float maxDecisionTime = 3f;
  private readonly float minDecisionTimePhase2 = 1f;
  private readonly float maxDecisionTimePhase2 = 2f;

  [Header("Laser Attack")]
  private bool laserActive = false;
  public float maxDistance = 200f;
  public LayerMask hitMask;

  public LayerMask groundMask;
  public LayerMask wallMask;

  public Transform origin;
  public Transform laserVisual;

  public GameObject ground;
  public GameObject[] wall;

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
    // if (owner.IsDead) return;

    // if (!canAttackTimer && isAttacking) return;
    // if (laserActive)
    // {
    //   Fire(Vector2.right);
    // }
  }

  void FixedUpdate()
  {
    if (owner.IsDead) return;

    if (!canAttackTimer && isAttacking) return;
    if (laserActive)
    {
      Fire(Vector2.right);
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
      // SwordArenaAttack,
      // SlashAttack,
      // GreatBallAttack
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

  // attack laser logic can be implemented here, including raycasting and visual effects
  void Fire(Vector2 direction)
  {
    RaycastHit2D hit = Physics2D.Raycast(origin.position, direction, maxDistance, hitMask);

    float distance = maxDistance;
    Vector2 hitPoint = (Vector2)origin.position + direction * maxDistance;

    if (hit.collider != null)
    {
      distance = hit.distance;
      hitPoint = hit.point;

      int hitLayer = hit.collider.gameObject.layer;

      // Detecta o tipo
      if ((groundMask & (1 << hitLayer)) != 0)
      {

      }
      else if ((wallMask & (1 << hitLayer)) != 0)
      {

      }
    }

    UpdateLaserVisual(distance, direction);
  }

  void UpdateLaserVisual(float length, Vector2 direction)
  {
    laserVisual.localScale = new Vector3(length, 1f, 1f);
    laserVisual.position = origin.position;
    laserVisual.right = direction;
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
    entity.Locomotion.FlipTowardsTarget(player);
    yield return new WaitForSeconds(1.2f);
    laserActive = true;
    yield return new WaitForSeconds(0.35f);
    laserActive = false;
    laserVisual.localScale = new Vector3(1f, 1f, 1f);
    yield return new WaitForSeconds(0.8f);
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
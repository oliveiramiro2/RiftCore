using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class AstralWeaverAttackModule : MonoBehaviour
{
  private Transform player;
  public bool isAttacking = false;
  [SerializeField] private float attackCooldown = 3f;
  [SerializeField] private GameObject laserPrefab;
  public bool canAttackTimer = true;
  private float attackTimer = 0f;

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {
    if (!canAttackTimer) return;
    isAttacking = false;
    attackTimer += Time.deltaTime;
    if (attackTimer >= attackCooldown)
    {
      isAttacking = true;
      attackTimer = 0f;
    }
  }

  public void DecideNextAttack(AstralWeaverController entity)
  {
    float dist = Vector2.Distance(transform.position, player.position);

    System.Collections.Generic.List<System.Action> validAttacks = new()
    {
      () => EnergyBall(entity),
      () => Laser(entity),
      () => MultiLasers(entity)
    };

    if (entity.Phase2())
    {
      validAttacks.Add(() => MultiLasers(entity));
    }

    int index = Random.Range(0, validAttacks.Count);
    validAttacks[index].Invoke();
  }

  public void EnergyBall(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);
    entity.AnimatorBridge.AstralWeaverEnergyBall();
    StartCoroutine(EnergyBallRoutine());
    Debug.Log("Energy Ball Attack");
  }

  private IEnumerator EnergyBallRoutine()
  {
    yield return new WaitForSeconds(2f);
    canAttackTimer = true;
  }

  public void Laser(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);

    entity.AnimatorBridge.AstralWeaverLaser();

    StartCoroutine(LaserRoutine(entity));
    Debug.Log("Laser Attack");
  }

  private IEnumerator LaserRoutine(AstralWeaverController entity)
  {
    float distance = Vector2.Distance(laserPrefab.transform.position, player.position);
    Vector2 dir = (player.position - laserPrefab.transform.position).normalized;
    float auxAngleCorretion = entity.transform.localScale.x >= 1 ? 0f : -180f;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + auxAngleCorretion;
    laserPrefab.transform.rotation = Quaternion.Euler(0, 0, angle);
    laserPrefab.transform.localScale = new Vector3(distance * 5, 0.2f, 1f);

    yield return new WaitForSeconds(0.8f);

    laserPrefab.SetActive(true);

    yield return new WaitForSeconds(1f);

    laserPrefab.SetActive(false);
    canAttackTimer = true;
  }

  public void MultiLasers(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);
    StartCoroutine(MultiLaserRoutine());
    Debug.Log("Multi Laser Attack");
  }

  private IEnumerator MultiLaserRoutine()
  {
    yield return new WaitForSeconds(2f);
    canAttackTimer = true;
  }
}
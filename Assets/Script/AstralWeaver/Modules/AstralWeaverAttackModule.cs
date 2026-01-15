using UnityEngine;
using System.Collections;

public class AstralWeaverAttackModule : MonoBehaviour
{
  private Transform player;
  public bool isAttacking = false;
  [SerializeField] private float attackCooldown = 3f;
  [SerializeField] private GameObject laserPrefab;
  private Material laserMaterial;
  public bool canAttackTimer = true;
  private float attackTimer = 0f;

  private bool lookingTarget = false;

  //
  private float distance, auxAngleCorretion, angle;
  private Vector2 dir;

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    laserMaterial = laserPrefab.GetComponent<SpriteRenderer>().material;
  }

  void Update()
  {
    if (lookingTarget)
    {
      AimTarget();
    }
    if (!canAttackTimer) return;
    isAttacking = false;
    attackTimer += Time.deltaTime;
    if (attackTimer >= attackCooldown)
    {
      isAttacking = true;
      attackTimer = 0f;
    }
  }

  void AimTarget()
  {
    distance = Vector2.Distance(laserPrefab.transform.position, player.position);
    dir = (player.position - laserPrefab.transform.position).normalized;
    auxAngleCorretion = transform.localScale.x >= 1 ? 0f : -180f;
    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + auxAngleCorretion;

    laserPrefab.transform.rotation = Quaternion.Euler(0, 0, angle);

    laserPrefab.transform.localScale = new Vector3(distance * 5, 0.2f, 1f);
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

  public void Laser(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);

    entity.AnimatorBridge.AstralWeaverLaser();

    StartCoroutine(LaserRoutine(entity));
    Debug.Log("Laser Attack");
  }

  public void MultiLasers(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);
    StartCoroutine(MultiLaserRoutine());
    Debug.Log("Multi Laser Attack");
  }

  private IEnumerator EnergyBallRoutine()
  {
    yield return new WaitForSeconds(2f);
    canAttackTimer = true;
  }

  private IEnumerator LaserRoutine(AstralWeaverController entity)
  {

    Hitbox laserCollider = laserPrefab.GetComponent<Hitbox>();

    laserCollider.Deactivate();

    lookingTarget = true;

    laserMaterial.SetFloat("_DistortionAmount", 0f);
    laserMaterial.SetFloat("_Alpha", 0.05f);
    laserPrefab.SetActive(true);


    yield return new WaitForSeconds(2f);

    lookingTarget = false;

    yield return new WaitForSeconds(0.2f);

    laserMaterial.SetFloat("_Alpha", 1f);
    laserMaterial.SetFloat("_DistortionAmount", 0.25f);
    laserCollider.Activate();

    yield return new WaitForSeconds(1f);
    laserMaterial.SetFloat("_DistortionAmount", 0f);
    laserCollider.Deactivate();
    laserPrefab.SetActive(false);
    canAttackTimer = true;
  }

  private IEnumerator MultiLaserRoutine()
  {
    yield return new WaitForSeconds(2f);
    canAttackTimer = true;
  }
}
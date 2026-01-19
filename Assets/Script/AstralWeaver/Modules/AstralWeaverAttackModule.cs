using UnityEngine;
using System.Collections;

public class AstralWeaverAttackModule : MonoBehaviour
{
  private AstralWeaverController owner;
  private Transform player;
  public bool isAttacking = false;
  [SerializeField] private float attackCooldown = 3f;
  [SerializeField] private GameObject laserPrefab;
  [SerializeField] private GameObject energyBallPrefab;
  [SerializeField] private GameObject[] crystalsPrefabs;
  [SerializeField] private Transform energyBallPosition;
  [SerializeField] private Transform[] crystalsPositions;
  [SerializeField] private LaserManager laserManager;
  [SerializeField] private float energyBallSpeed;

  [Header("Decision Timers")]
  [SerializeField] private float minDecisionTime = 2f;
  [SerializeField] private float maxDecisionTime = 4f;
  [SerializeField] private float minDecisionTimePhase2 = 1f;
  [SerializeField] private float maxDecisionTimePhase2 = 3f;

  private Material laserMaterial;
  public bool canAttackTimer = true;
  private float attackTimer = 0f;

  private bool lookingTarget = false;
  private bool energyBallFollow = false;

  private float distance, auxAngleCorretion, angle;
  private Vector2 dir, targetPos;
  private Vector3 shootDir;
  private readonly float crystalSpeed = 15f;
  private readonly float[] angles = { 20f, 0f, -20f };

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    laserMaterial = laserPrefab.GetComponent<SpriteRenderer>().material;
  }

  void Update()
  {
    if (energyBallFollow)
    {
      LockTargetEnergyBall();
    }
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

  public void Initialize(AstralWeaverController controller)
  {
    owner = controller;
  }

  public void DecideNextAttack(AstralWeaverController entity)
  {
    float dist = Vector2.Distance(transform.position, player.position);

    System.Collections.Generic.List<System.Action> validAttacks = new()
    {
      () => EnergyBall(entity),
      () => Laser(entity),
      () => Crystals(entity),
      () => MultiLasers(entity),
      () => Shield(entity)
    };

    if (entity.Phase2())
    {
      validAttacks.Add(() => MultiLasers(entity));
      validAttacks.Add(() => Crystals(entity));
    }

    int index = Random.Range(0, validAttacks.Count);
    validAttacks[index].Invoke();
  }

  void AimTarget()
  {
    distance = Vector2.Distance(laserPrefab.transform.position, player.position);
    dir = (player.position - laserPrefab.transform.position).normalized;
    auxAngleCorretion = transform.localScale.x >= 1 ? 0f : -180f;
    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + auxAngleCorretion;

    laserPrefab.transform.rotation = Quaternion.Euler(0, 0, angle);

    laserPrefab.transform.localScale = new Vector3(distance * 6, 0.1f, 1f);
  }

  void LockTargetEnergyBall()
  {
    targetPos = player.position;
    dir = (targetPos - (Vector2)energyBallPrefab.transform.position).normalized;
    energyBallPrefab.transform.localScale = new Vector3(dir.x > 0 ? 1 : -1, 1, 1);
    energyBallPrefab.GetComponent<Rigidbody2D>().linearVelocity = dir * energyBallSpeed;
  }

  public void EnergyBall(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);
    entity.AnimatorBridge.AstralWeaverEnergyBall();
    StartCoroutine(EnergyBallRoutine(entity));
  }

  public void Laser(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);

    entity.AnimatorBridge.AstralWeaverLaser();

    StartCoroutine(LaserRoutine(entity));
  }

  public void MultiLasers(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);
    StartCoroutine(MultiLaserRoutine(entity));
  }

  public void Shield(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);
    AstralWeaverDamageHandler damageControl = entity.GetComponent<AstralWeaverDamageHandler>();
    damageControl.shieldIsActive = true;
    entity.awEvents.OnShield.Raise();
    StartCoroutine(ShieldRoutine(entity));
  }

  public void Crystals(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);
    StartCoroutine(CrystalsRoutine(entity));
  }

  private IEnumerator EnergyBallRoutine(AstralWeaverController entity)
  {
    Hitbox auxControl = energyBallPrefab.GetComponent<Hitbox>();

    entity.LocomotionModule.canFlip = false;
    energyBallPrefab.transform.position = energyBallPosition.position;
    energyBallPrefab.transform.localScale = owner.transform.localScale;

    yield return new WaitForSeconds(0.5f);

    energyBallPrefab.SetActive(true);
    auxControl.Activate();
    entity.awEvents.OnEnergyBall.Raise();

    yield return new WaitForSeconds(0.5f);

    energyBallFollow = true;

    yield return new WaitForSeconds(2f);

    energyBallFollow = false;
    auxControl.Deactivate();
    energyBallPrefab.SetActive(false);
    canAttackTimer = true;
    entity.LocomotionModule.canFlip = true;
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
    entity.LocomotionModule.canFlip = false;
    entity.awEvents.OnLaser.Raise();

    yield return new WaitForSeconds(0.3f);

    laserMaterial.SetFloat("_Alpha", 1f);
    laserMaterial.SetFloat("_DistortionAmount", 1f);
    laserCollider.Activate();

    yield return new WaitForSeconds(1f);
    laserCollider.Deactivate();
    laserPrefab.SetActive(false);
    canAttackTimer = true;
    entity.LocomotionModule.canFlip = true;
  }

  private IEnumerator MultiLaserRoutine(AstralWeaverController entity)
  {
    entity.LocomotionModule.canFlip = false;
    entity.AnimatorBridge.AstralWeaverMultiLasers();

    yield return new WaitForSeconds(1f);
    entity.awEvents.OnMultiLasers.Raise();
    laserManager.LasersStart();


    yield return new WaitForSeconds(4f);
    entity.awEvents.OnMultiLasers.Raise();
    laserManager.LasersStart();


    yield return new WaitForSeconds(4f);
    canAttackTimer = true;
    entity.LocomotionModule.canFlip = true;
  }

  private IEnumerator ShieldRoutine(AstralWeaverController entity)
  {
    entity.AnimatorBridge.AstralWeaverShield();
    yield return new WaitForSeconds(2f);
    canAttackTimer = true;
  }

  private IEnumerator CrystalsRoutine(AstralWeaverController entity)
  {
    entity.AnimatorBridge.AstralWeaverCrystals();

    yield return new WaitForSeconds(1.5f);

    Vector2 baseDir =
        (player.position - transform.position).normalized;

    entity.LocomotionModule.FlipTowardsTarget(entity);

    entity.awEvents.OnCrystals.Raise();

    for (int i = 0; i < crystalsPrefabs.Length; i++)
    {
      crystalsPrefabs[i].transform.position = crystalsPositions[i].transform.position;
      crystalsPrefabs[i].SetActive(true);

      shootDir = Quaternion.Euler(0, 0, angles[i]) * baseDir;


      Rigidbody2D rb = crystalsPrefabs[i].GetComponent<Rigidbody2D>();
      rb.linearVelocity = shootDir * crystalSpeed;
    }


    yield return new WaitForSeconds(1f);
    for (int i = 0; i < crystalsPrefabs.Length; i++)
    {
      crystalsPrefabs[i].GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
      crystalsPrefabs[i].SetActive(false);
    }
    canAttackTimer = true;
  }
}
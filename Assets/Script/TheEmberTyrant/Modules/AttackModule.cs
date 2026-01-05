using UnityEngine;

public class AttackModule : MonoBehaviour
{
  private BossController controller;

  [Header("References")]
  public Transform player;
  public FirePillar firePillarPrefab;
  public Transform[] firePillarSpawnPoint;
  public FireBall fireBallPrefab;
  public Transform fireBallSpawnPoint;

  [Header("Ranges")]
  public float meleeRange = 2f;

  [Header("Decision Timers")]
  public float minDecisionTime = 1f;
  public float maxDecisionTime = 2f;
  public float minDecisionTimePhase2 = 1f;
  public float maxDecisionTimePhase2 = 2f;

  [Header("Attack Flag")]
  public bool canAttack = false;
  public bool finishAttack = false;
  public bool wantsToAttack;
  public bool isAttacking = false;

  private float decisionTimer;
  public bool attackRequested;

  public void RequestAttack()
  {
    attackRequested = true;
  }

  public void ConsumeAttackRequest()
  {
    attackRequested = false;
  }

  public void initialize(BossController controller)
  {
    this.controller = controller;
  }

  private void Start()
  {
    ResetTimer();
  }

  private void Update()
  {
    if (!finishAttack)
      return;

    decisionTimer -= Time.unscaledDeltaTime;

    if (decisionTimer <= 0f)
    {
      RequestAttack();
    }
  }


  public void ResetTimer()
  {
    if (controller.Phase2())
    {
      decisionTimer = Random.Range(minDecisionTimePhase2, maxDecisionTimePhase2);
    }
    else
    {
      decisionTimer = Random.Range(minDecisionTime, maxDecisionTime);
    }
  }

  public void DecideNextAttack(BossController entity)
  {
    float dist = Vector2.Distance(transform.position, player.position);

    System.Collections.Generic.List<System.Action> validAttacks = new();

    if (dist <= meleeRange)
      validAttacks.Add(() => MeleeAttack(entity));

    validAttacks.Add(() => FireDashAttack(entity));

    validAttacks.Add(() => FireBallAttack(entity));

    if (entity.Phase2())
    {
      validAttacks.Add(() => FireExplosionAttack(entity));
      validAttacks.Add(() => FirePillarAttack(entity));
    }

    if (validAttacks.Count == 0)
      validAttacks.Add(entity.AnimatorBridge.TETPunch);

    int index = Random.Range(0, validAttacks.Count);
    validAttacks[index].Invoke();
  }

  public void MeleeAttack(BossController entity)
  {
    entity.AnimatorBridge.TETPunch();
    entity.tetEvents.OnPunch.Raise();
  }

  public void FireBallAttack(BossController entity)
  {
    entity.AnimatorBridge.TETFireball();
    entity.tetEvents.OnFireBall.Raise();
  }

  public void FireBallSpawn()
  {
    var fireball = Instantiate(fireBallPrefab, fireBallSpawnPoint.position, Quaternion.identity)
    .GetComponent<FireBall>();

    Vector2 dir = transform.localScale.x > 0 ? Vector2.right : Vector2.left; // olhando
    fireball.SetDirection(dir);
  }

  public void FirePillarAttack(BossController entity)
  {
    entity.AnimatorBridge.TETFirepillar();
    foreach (var spawnPoint in firePillarSpawnPoint)
      Instantiate(firePillarPrefab, spawnPoint.position, Quaternion.identity);

    entity.tetEvents.OnFirePillar.Raise();
  }

  public void FireExplosionAttack(BossController entity)
  {
    entity.AnimatorBridge.TETFireblast();
  }

  public void FireDashAttack(BossController entity)
  {
    entity.AnimatorBridge.TETFiredash();
    entity.tetEvents.OnDash.Raise();
  }
}

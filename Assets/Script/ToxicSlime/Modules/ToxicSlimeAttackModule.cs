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
      () => attack1(entity),
      () => attack2(entity),
      () => attack3(entity)
    };

    if (entity.Phase2())
    {
      validAttacks.Add(() => attack4(entity));
      validAttacks.Add(() => attack5(entity));
    }

    int index = Random.Range(0, validAttacks.Count);

    if (owner.IsDead) return;

    validAttacks[index].Invoke();
  }

  private void attack1(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 1");
  }

  private void attack2(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 2");
  }

  private void attack3(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 3");
  }

  private void attack4(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 4");
  }

  private void attack5(ToxicSlimeController entity)
  {
    Debug.Log("ToxicSlime Attack 5");
  }
}
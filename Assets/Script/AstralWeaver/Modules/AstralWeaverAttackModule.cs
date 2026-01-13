using UnityEngine;

public class AstralWeaverAttackModule : MonoBehaviour
{
  private Transform player;
  public bool isAttacking = false;
  [SerializeField] private float attackCooldown = 3f;
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
    entity.AnimatorBridge.AstralWeaverEnergyBall();
    Debug.Log("Energy Ball Attack");
  }

  public void Laser(AstralWeaverController entity)
  {
    entity.AnimatorBridge.AstralWeaverLaser();
    Debug.Log("Laser Attack");
  }

  public void MultiLasers(AstralWeaverController entity)
  {
    //entity.AnimatorBridge.AWMeatballSwipe();
    Debug.Log("Multi Laser Attack");
  }
}
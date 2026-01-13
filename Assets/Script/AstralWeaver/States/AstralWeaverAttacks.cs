using UnityEngine;

public class AstralWeaverAttacks : State<AstralWeaverController>
{
  private float attackDuration = 1.5f;
  private float attackTimer;
  public override void EnterState(AstralWeaverController entity)
  {
    entity.AttackModule.canAttackTimer = false;
    attackTimer = 0f;
    entity.AttackModule.DecideNextAttack(entity);
  }

  public override void UpdateState(AstralWeaverController entity)
  {
    Debug.Log("AstralWeaver is Attacking");
    attackTimer += Time.unscaledDeltaTime;
    if (attackTimer >= attackDuration)
    {
      entity.AttackModule.canAttackTimer = true;
    }
  }

  public override void ExitState(AstralWeaverController entity)
  {

  }
}

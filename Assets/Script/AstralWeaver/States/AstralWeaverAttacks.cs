using UnityEngine;

public class AstralWeaverAttacks : State<AstralWeaverController>
{
  public override void EnterState(AstralWeaverController entity)
  {
    entity.AttackModule.canAttackTimer = false;
    AstralWeaverDamageHandler damageControl = entity.GetComponent<AstralWeaverDamageHandler>();
  
    damageControl.shieldIsActive = false;
    entity.AttackModule.DecideNextAttack(entity);
  }

  public override void UpdateState(AstralWeaverController entity)
  {
  }

  public override void ExitState(AstralWeaverController entity)
  {

  }
}

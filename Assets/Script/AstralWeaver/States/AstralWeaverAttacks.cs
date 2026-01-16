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
    // Debug.Log("AstralWeaver is Attacking");
    // attackTimer += Time.unscaledDeltaTime;
    // if (attackTimer >= attackDuration)
    // {
    //   entity.AttackModule.canAttackTimer = true;
    // }
  }

  public override void ExitState(AstralWeaverController entity)
  {

  }
}

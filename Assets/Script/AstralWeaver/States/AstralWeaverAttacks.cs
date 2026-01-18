using UnityEngine;

public class AstralWeaverAttacks : State<AstralWeaverController>
{
  private bool canDecideNextAttack = true;
  private float timer = 0.5f;

  public override void EnterState(AstralWeaverController entity)
  {
    entity.AttackModule.canAttackTimer = false;
    AstralWeaverDamageHandler damageControl = entity.GetComponent<AstralWeaverDamageHandler>();

    if (damageControl.shieldIsActive)
    {
      entity.AnimatorBridge.AstralWeaverShieldEnd();
      canDecideNextAttack = false;
    }
    damageControl.shieldIsActive = false;

    if (canDecideNextAttack)
      entity.AttackModule.DecideNextAttack(entity);
  }

  public override void UpdateState(AstralWeaverController entity)
  {
    if (!canDecideNextAttack)
    {
      timer -= Time.deltaTime;
      if (timer <= 0f)
      {
        canDecideNextAttack = true;
        entity.AttackModule.DecideNextAttack(entity);
      }
    }

  }

  public override void ExitState(AstralWeaverController entity)
  {

  }
}

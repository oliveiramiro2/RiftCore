using UnityEngine;

public class AstralWeaverIdle : State<AstralWeaverController>
{

  public override void EnterState(AstralWeaverController entity)
  {
    AstralWeaverDamageHandler damageControl = entity.GetComponent<AstralWeaverDamageHandler>();
    if (!damageControl.shieldIsActive)
      entity.AnimatorBridge.AstralWeaverIdle();
  }

  public override void UpdateState(AstralWeaverController entity)
  {
  }

  public override void ExitState(AstralWeaverController entity)
  {
    entity.LocomotionModule.TeleportToRandomPoint(entity);
  }
}

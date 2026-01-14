using UnityEngine;

public class AstralWeaverIdle : State<AstralWeaverController>
{

  public override void EnterState(AstralWeaverController entity)
  {
    entity.AnimatorBridge.AstralWeaverIdle();
  }

  public override void UpdateState(AstralWeaverController entity)
  {
    entity.LocomotionModule.FlipTowardsTarget(entity);
  }

  public override void ExitState(AstralWeaverController entity)
  {
    entity.LocomotionModule.TeleportToRandomPoint(entity);
  }
}

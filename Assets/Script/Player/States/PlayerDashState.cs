using UnityEngine;


public class PlayerDashState : State<PlayerController>
{

  public override void EnterState(PlayerController entity)
  {
    entity.AnimatorBridge.TriggerDash();
    entity.AnimatorBridge.SetDashing(true);
    entity.IFrames.EnableIFrames(entity.hurtboxCollider);
    entity.DashModule.ApplyDash();
    entity.dashHitbox.Activate();
  }

  public override void UpdateState(PlayerController entity)
  {
    // Nothing
  }


  public override void ExitState(PlayerController entity)
  {
    entity.IFrames.DisableIFrames(entity.hurtboxCollider);
    entity.AnimatorBridge.ResetTriggerDash();
    entity.AnimatorBridge.SetDashing(false);
    entity.dashHitbox.Deactivate();
    entity.DashModule.EndDash(entity);
  }
}
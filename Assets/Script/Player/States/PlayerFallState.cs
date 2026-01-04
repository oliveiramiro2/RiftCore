using UnityEngine;

public class PlayerFallState : State<PlayerController>
{
  public override void EnterState(PlayerController entity)
  {
    entity.AnimatorBridge.ResetTriggerLand();
    entity.AnimatorBridge.SetFalling(true);
    entity.AnimatorBridge.TriggerFall();
  }

  public override void UpdateState(PlayerController entity)
  {
    // MOVIMENTO NO AR
    entity.LocomotionModule.MoveAirborne(entity.InputReader.MoveInput.x);
  }

  public override void ExitState(PlayerController entity)
  {
    entity.AnimatorBridge.SetFalling(false);
    entity.events.OnLand.Raise();
    entity.AnimatorBridge.ResetTriggerFall();
    entity.AnimatorBridge.TriggerLand();
  }
}


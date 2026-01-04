using UnityEngine;

public class PlayerJumpState : State<PlayerController>
{
  public override void EnterState(PlayerController entity)
  {
    entity.LocomotionModule.ApplyJumpForce();
    //entity.AnimatorBridge.SetJumping(true);
    entity.AnimatorBridge.TriggerJump();
    entity.events.OnJump.Raise();
  }

  public override void UpdateState(PlayerController entity)
  {
    // MOVIMENTO NO AR
    entity.LocomotionModule.MoveAirborne(entity.InputReader.MoveInput.x);

    // Se o botão NÃO está sendo segurado E o boneco ainda está subindo
    if (!entity.InputReader.JumpHeld && entity.PhysicsModule.rb.linearVelocityY > 0)
    {
      // Chama o módulo para cortar a velocidade
      entity.JumpModule.CancelJump();
    }
  }

  public override void ExitState(PlayerController entity)
  {
    //entity.AnimatorBridge.SetJumping(false);
    entity.AnimatorBridge.ResetTriggerJump();
  }
}


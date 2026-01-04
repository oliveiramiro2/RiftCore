using UnityEngine;


public class PlayerRunState : State<PlayerController>
{
  public float runSpeedMultiplier = 1f;


  public override void EnterState(PlayerController entity)
  {
    base.EnterState(entity);
    entity.AnimatorBridge.SetMoveSpeed(1f);
  }


  public override void UpdateState(PlayerController entity)
  {
    entity.LocomotionModule.FootstepSoundTick();
    float dir = entity.InputReader.MoveInput.x;

    if (!entity.canMove) return;
    entity.PhysicsModule.MoveHorizontal(dir * entity.baseMoveSpeed * runSpeedMultiplier);
    entity.FlipX(dir > 0);
  }


  public override void ExitState(PlayerController entity)
  {
    base.ExitState(entity);
    entity.PhysicsModule.StopHorizontal();
    entity.AnimatorBridge.SetMoveSpeed(0f);
  }
}
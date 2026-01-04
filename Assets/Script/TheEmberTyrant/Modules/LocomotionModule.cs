using UnityEngine;

public class LocomotionModule : MonoBehaviour
{
  public void Patroling(BossController entity)
  {
    if (!entity.BossPhysics.IsWallAhead())
    {
      float targetSpeed = entity.IsFacingRight() ? entity.MoveSpeed : -entity.MoveSpeed;
      entity.BossPhysics.TETMoveHorizontal(targetSpeed);
    }
    else
    {
      entity.BossPhysics.TETStopHorizontal();
      entity.FlipX(!entity.IsFacingRight());
    }
  }

  public void FlipTowardsTarget(BossController entity)
  {
    if (entity.TargetingModule.player != null)
    {
      bool shouldFaceRight = entity.TargetingModule.player.position.x > entity.transform.position.x;
      entity.FlipX(shouldFaceRight);
    }
  }
}
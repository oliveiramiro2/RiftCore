using UnityEngine;

public class ToxicSlimeLocomotionModule : MonoBehaviour
{

  private ToxicSlimeController owner;

  public void Setup(ToxicSlimeController entity)
  {
    owner = entity;
  }

  public void Patroling(ToxicSlimeController entity)
  {
    float targetSpeed = entity.IsFacingRight() ? entity.MoveSpeed : -entity.MoveSpeed;
    entity.ToxicSlimePhysics.ToxicSlimeMoveHorizontal(targetSpeed);
  }

  public void FlipTowardsTarget(ToxicSlimeController entity)
  {
    if (entity.PlayerTransform != null)
    {
      bool shouldFaceRight = entity.PlayerTransform.position.x > entity.transform.position.x;
      entity.FlipX(shouldFaceRight);
    }
  }

  public void BossCanRoll()
  {
    owner.canRoll = true;
  }

  public void BossCannotRoll()
  {
    owner.canRoll = false;
  }
}
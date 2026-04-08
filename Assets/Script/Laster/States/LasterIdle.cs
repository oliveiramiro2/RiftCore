using UnityEngine;

public class LasterIdle : State<LasterController>
{
  private float timer = 0f;
  private readonly float idleDuration = 2f;

  public override void EnterState(LasterController entity)
  {
    entity.Locomotion.FlipTowardsTarget(entity.PlayerTransform);
    entity.AnimatorBridge.LasterIdle();
    timer = 0f;

    Debug.Log("Entering Idle State");
  }

  public override void UpdateState(LasterController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {

      entity.isAttacking = true;
    }
  }

  public override void ExitState(LasterController entity)
  {
  }
}

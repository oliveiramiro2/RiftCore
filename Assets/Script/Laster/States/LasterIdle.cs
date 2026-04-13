using UnityEngine;

public class LasterIdle : State<LasterController>
{
  private float timer = 0f;

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
    if (timer >= entity.Attack.attackCooldown)
    {
      entity.isAttacking = true;
    }
  }

  public override void ExitState(LasterController entity)
  {
  }
}

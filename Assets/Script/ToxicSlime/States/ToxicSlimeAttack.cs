using UnityEngine;

public class ToxicSlimeAttack : State<ToxicSlimeController>
{
  private float timer = 0f;
  private float idleDuration = 2f;

  public override void EnterState(ToxicSlimeController entity)
  {
    Debug.Log("Enter ToxicSlime Attack State");
    timer = 0f;
    entity.AnimatorBridge.ToxicSlimeBallStart();
  }

  public override void UpdateState(ToxicSlimeController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {
      entity.isAttacking = false;
      entity.AnimatorBridge.ToxicSlimeBallEnd();
    }
    Debug.Log($"can roll: {entity.canRoll}");
    entity.ToxicSlimeLocomotionModule.FlipTowardsTarget(entity);
    if (entity.canRoll)
      entity.ToxicSlimeLocomotionModule.Patroling(entity);
  }

  public override void ExitState(ToxicSlimeController entity)
  {
    entity.ToxicSlimePhysics.ToxicSlimeStopHorizontal();
  }
}

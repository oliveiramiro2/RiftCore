using UnityEngine;

public class ToxicSlimeAttack : State<ToxicSlimeController>
{
  private float timer = 0f;
  private float idleDuration = 2f;
  private bool hasRolled = false;

  public override void EnterState(ToxicSlimeController entity)
  {
    Debug.Log("Enter ToxicSlime Attack State");
    timer = 0f;
    entity.AnimatorBridge.ToxicSlimeBallStart();
    hasRolled = false;
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
  
    if (entity.canRoll && !hasRolled)
    {
      hasRolled = true;
      entity.ToxicSlimeLocomotionModule.Roll(1f, 2f);
      entity.ToxicSlimeLocomotionModule.Patroling(entity);
    }
  }

  public override void ExitState(ToxicSlimeController entity)
  {
    entity.ToxicSlimePhysics.ToxicSlimeStop();
  }
}

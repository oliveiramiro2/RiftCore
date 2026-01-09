using UnityEngine;

public class TETAttack : State<BossController>
{
  private readonly float timeToExit = 1.5f;
  private float timer = 0f;
  public override void EnterState(BossController entity)
  {
    timer = timeToExit;
    entity.AttackModule.ConsumeAttackRequest();
    entity.AttackModule.finishAttack = false;
    entity.LocomotionModule.FlipTowardsTarget(entity);
    entity.AttackModule.DecideNextAttack(entity);
  }

  public override void UpdateState(BossController entity)
  {
    timer -= Time.unscaledDeltaTime;
    if (entity.AnimatorBridge.TETIsCurrentAnimationFinished() && timer <= 0f)
    {
      entity.AttackModule.finishAttack = true;
    }
  }

  public override void ExitState(BossController entity)
  {
    entity.AttackModule.ResetTimer();
  }
}

using UnityEngine;

public class TETAttack : State<BossController>
{

  public override void EnterState(BossController entity)
  {
    entity.AttackModule.ConsumeAttackRequest();
    entity.AttackModule.finishAttack = false;
    entity.LocomotionModule.FlipTowardsTarget(entity);
    entity.AttackModule.DecideNextAttack(entity);
  }

  public override void UpdateState(BossController entity)
  {
    if (entity.AnimatorBridge.TETIsCurrentAnimationFinished())
    {
      entity.AttackModule.finishAttack = true;
    }
  }

  public override void ExitState(BossController entity)
  {
    entity.AttackModule.ResetTimer();
  }
}

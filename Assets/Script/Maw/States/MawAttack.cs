using UnityEngine;

public class MawAttack : State<MawController>
{
  public override void EnterState(MawController entity)
  {
    entity.Locomotion.FlipTowardsTarget(entity.PlayerTransform);
    entity.Attack.DecideNextAttack(entity);
  }

  public override void UpdateState(MawController entity)
  {

  }

  public override void ExitState(MawController entity)
  {
  }
}

using UnityEngine;

public class MawAttack : State<MawController>
{
  private float timer = 0f;
  private readonly float idleDuration = 2f;
  public override void EnterState(MawController entity)
  {
    timer = 0f;
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

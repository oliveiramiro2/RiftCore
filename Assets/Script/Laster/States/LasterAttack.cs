using UnityEngine;

public class LasterAttack : State<LasterController>
{

  public override void EnterState(LasterController entity)
  {
    entity.Locomotion.FlipTowardsTarget(entity.PlayerTransform);

    entity.Attack.DecideNextAttack(entity);

    Debug.Log("Entering Attack State");
  }

  public override void UpdateState(LasterController entity)
  {

  }

  public override void ExitState(LasterController entity)
  {
  }
}

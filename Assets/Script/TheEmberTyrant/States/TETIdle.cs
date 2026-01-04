using System;
using UnityEngine;

public class TETIdle : State<BossController>
{

  public override void EnterState(BossController entity)
  {
    entity.AnimatorBridge.TETIdle();
  }

  public override void UpdateState(BossController entity)
  {

    entity.LocomotionModule.FlipTowardsTarget(entity);
  }

  public override void ExitState(BossController entity)
  {
  }
}

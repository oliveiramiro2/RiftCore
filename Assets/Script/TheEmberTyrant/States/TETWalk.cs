using System;
using UnityEngine;

public class TETWalk : State<BossController>
{

  public override void EnterState(BossController entity)
  {
    entity.AnimatorBridge.TETRun();
  }

  public override void UpdateState(BossController entity)
  {
    entity.LocomotionModule.Patroling(entity);

  }

  public override void ExitState(BossController entity)
  {
    entity.BossPhysics.TETStopHorizontal();
  }
}

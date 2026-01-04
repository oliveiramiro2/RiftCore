using System;
using UnityEngine;

public class PlayerIdleState : State<PlayerController>
{
  public override void EnterState(PlayerController entity)
  {
    entity.AnimatorBridge.SetMoveSpeed(0f);
  }

  public override void UpdateState(PlayerController entity)
  { }

  public override void ExitState(PlayerController entity)
  {
    entity.AnimatorBridge.SetMoveSpeed(entity.InputReader.MoveInput.x);
  }
}

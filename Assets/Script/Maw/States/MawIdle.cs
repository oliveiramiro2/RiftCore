using System;
using UnityEngine;

public class MawIdle : State<MawController>
{
  private float timer = 0f;
  private readonly float idleDuration = 2f;

  public override void EnterState(MawController entity)
  {
    entity.AnimatorBridge.MawIdle();
    timer = 0f;
    
    Debug.Log("Entering Idle State");
  }

  public override void UpdateState(MawController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {
      entity.isAttacking = true;
    }
  }

  public override void ExitState(MawController entity)
  {
  }
}

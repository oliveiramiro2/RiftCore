using UnityEngine;

public class SwordMasterIdle : State<SwordMasterController>
{
  private float timer = 0f;
  private float idleDuration = 2f;

  public override void EnterState(SwordMasterController entity)
  {
    entity.AnimatorBridge.SwordMasterIdle();
    timer = 0f;
  }

  public override void UpdateState(SwordMasterController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {
      entity.isAttacking = true;
    }
  }

  public override void ExitState(SwordMasterController entity)
  {
  }
}

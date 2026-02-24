using UnityEngine;

public class SwordMasterAttack : State<SwordMasterController>
{

  private float timer = 0f;
  private float idleDuration = 2f;

  public override void EnterState(SwordMasterController entity)
  {
    timer = 0f;
    entity.AnimatorBridge.SwordMasterRun();
    Debug.Log("Entered Attack State");
  }

  public override void UpdateState(SwordMasterController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {
      entity.isAttacking = false;
    }
  }

  public override void ExitState(SwordMasterController entity)
  {

  }
}

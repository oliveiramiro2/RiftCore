using UnityEngine;

public class SwordMasterDeath : State<SwordMasterController>
{

  public override void EnterState(SwordMasterController entity)
  {
    Debug.Log("Entering Death State");
    entity.AnimatorBridge.SwordMasterDeath();
  }

  public override void UpdateState(SwordMasterController entity)
  {

  }

  public override void ExitState(SwordMasterController entity)
  {
  }
}

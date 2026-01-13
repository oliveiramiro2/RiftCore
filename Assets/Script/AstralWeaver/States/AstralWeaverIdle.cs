using UnityEngine;

public class AstralWeaverIdle : State<AstralWeaverController>
{

  public override void EnterState(AstralWeaverController entity)
  {
    entity.AnimatorBridge.AstralWeaverIdle();
  }


  public override void UpdateState(AstralWeaverController entity)
  {
    //Debug.Log("AstralWeaver is Idling");
    //
  }

  public override void ExitState(AstralWeaverController entity)
  {
  }
}

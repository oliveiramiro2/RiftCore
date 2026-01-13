using UnityEngine;

public class AstralWeaverIdle : State<AstralWeaverController>
{

  public override void EnterState(AstralWeaverController entity)
  {
    entity.AnimatorBridge.AstralWeaverIdle();
  }
}

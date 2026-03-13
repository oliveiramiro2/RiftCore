using UnityEngine;

public class MawDeath : State<MawController>
{

  public override void EnterState(MawController entity)
  {
    Debug.Log("Entering Death State");
  }

  public override void UpdateState(MawController entity)
  {

  }

  public override void ExitState(MawController entity)
  {
  }
}

using UnityEngine;

public class SwordMasterAttack : State<SwordMasterController>
{

  public override void EnterState(SwordMasterController entity)
  {
    entity.Attack.DecideNextAttack(entity);
  }

  public override void UpdateState(SwordMasterController entity)
  {
    
  }

  public override void ExitState(SwordMasterController entity)
  {
  }
}

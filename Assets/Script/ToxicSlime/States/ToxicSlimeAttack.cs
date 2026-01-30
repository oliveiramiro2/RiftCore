using UnityEngine;

public class ToxicSlimeAttack : State<ToxicSlimeController>
{

  public override void EnterState(ToxicSlimeController entity)
  {
    entity.Attack.DecideNextAttack(entity);
  }

  public override void UpdateState(ToxicSlimeController entity)
  {
  }

  public override void ExitState(ToxicSlimeController entity)
  {

  }
}

using UnityEngine;

public class ToxicSlimeAttack : State<ToxicSlimeController>
{
  private float timer = 0f;
  private float idleDuration = 3f;

  public override void EnterState(ToxicSlimeController entity)
  {
    timer = 0f;
    entity.Attack.DecideNextAttack(entity);
  }

  public override void UpdateState(ToxicSlimeController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {
      entity.isAttacking = false;
    }
  }

  public override void ExitState(ToxicSlimeController entity)
  {

  }
}

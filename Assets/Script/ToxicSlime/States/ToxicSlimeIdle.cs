using UnityEngine;

public class ToxicSlimeIdle : State<ToxicSlimeController>
{
  private float timer = 0f;
  private float idleDuration = 2f;

  public override void EnterState(ToxicSlimeController entity)
  {
    Debug.Log("Enter ToxicSlime Idle State");
    timer = 0f;
  }

  public override void UpdateState(ToxicSlimeController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {
      entity.isAttacking = true;
    }
  }

  public override void ExitState(ToxicSlimeController entity)
  {
  }
}

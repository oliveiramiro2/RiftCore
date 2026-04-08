using UnityEngine;

public class LasterAttack : State<LasterController>
{
  private float timer = 0f;
  private readonly float attackDuration = 2f;

  public override void EnterState(LasterController entity)
  {
    entity.Locomotion.FlipTowardsTarget(entity.PlayerTransform);

    timer = 0f;

    Debug.Log("Entering Attack State");
  }

  public override void UpdateState(LasterController entity)
  {
    timer += Time.deltaTime;
    if (timer >= attackDuration)
    {

      entity.isAttacking = false;

    }
  }

  public override void ExitState(LasterController entity)
  {
  }
}

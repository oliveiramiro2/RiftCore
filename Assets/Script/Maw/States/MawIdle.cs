using UnityEngine;

public class MawIdle : State<MawController>
{
  private float timer = 0f;
  private readonly float idleDuration = 2f;

  public override void EnterState(MawController entity)
  {
    entity.Locomotion.FlipTowardsTarget(entity.PlayerTransform);
    entity.AnimatorBridge.MawIdle();
    timer = 0f;

    Debug.Log("Entering Idle State");
  }

  public override void UpdateState(MawController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {
      float randomValue = Random.Range(0f, 1f);
      if (randomValue < 0.5f)
      {
        entity.isAttacking = true;
      }
      else
      {
        entity.canFollowPlayer = false;
      }
    }
  }

  public override void ExitState(MawController entity)
  {
  }
}

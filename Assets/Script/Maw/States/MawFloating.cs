using UnityEngine;

public class MawFloating : State<MawController>
{
  private readonly float floatDuration = 2f, floatOutDuration = 0.5f;
  private float timer = 0f;
  private bool pauseTimer = false;


  public override void EnterState(MawController entity)
  {
    Debug.Log("Entering Floating State");
    entity.AnimatorBridge.MawFloatIn();
    timer = 0f;
    entity.Locomotion.FlipTowardsTarget(entity.PlayerTransform);
    entity.canFollowPlayer = true;
  }

  public override void UpdateState(MawController entity)
  {
    timer += Time.deltaTime;
    if (timer >= floatDuration)
    {
      if (!pauseTimer)
      {
        entity.AnimatorBridge.MawFloatOut();
        pauseTimer = true;
        timer -= floatOutDuration;
        return;
      }
      float randomValue = Random.Range(0f, 1f);
      if (randomValue < 0.5f)
      {
        entity.isAttacking = true;
      }
      entity.canFollowPlayer = false;
    }
  }

  public override void ExitState(MawController entity)
  {
    entity.isMoving = false;
  }
}

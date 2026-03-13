using UnityEngine;

public class MawFloating : State<MawController>
{
  private readonly float floatOutDuration = 0.5f, hideStaffDuration = 2f;
  private float timer = 0f, floatDuration = 4f;
  private bool pauseTimer = false;


  public override void EnterState(MawController entity)
  {
    Debug.Log("Entering Floating State");
    timer = 0f;
    entity.Locomotion.FlipTowardsTarget(entity.PlayerTransform);

    float randomValue = Random.Range(0f, 1f);
    if (randomValue < 0.5f)
    {
      entity.canFollowPlayer = true;
      entity.Locomotion.canFloat = true;
      if (entity.hasStaffSummoned)
        floatDuration += hideStaffDuration;
    }
    else
    {
      entity.Locomotion.hasTeleported = false;
      entity.canTeleport = true;
      entity.Locomotion.canFloat = false;
    }

  }

  public override void UpdateState(MawController entity)
  {
    timer += Time.deltaTime;
    if (timer >= floatDuration && !entity.canTeleport)
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

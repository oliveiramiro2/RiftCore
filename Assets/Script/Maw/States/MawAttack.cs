using UnityEngine;

public class MawAttack : State<MawController>
{
  private float timer = 0f;
  private readonly float idleDuration = 2f;
  public override void EnterState(MawController entity)
  {
    timer = 0f;
    Debug.Log("Entering Attack State");
  }

  public override void UpdateState(MawController entity)
  {
    timer += Time.deltaTime;
    if (timer >= idleDuration)
    {
      float randomValue = Random.Range(0f, 1f);
      if (randomValue < 0.5f)
        entity.canFollowPlayer = true;

      entity.isAttacking = false;
    }
  }

  public override void ExitState(MawController entity)
  {
  }
}

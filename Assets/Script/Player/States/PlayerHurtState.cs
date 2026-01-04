using UnityEngine;


public class PlayerHurtState : State<PlayerController>
{
  private float timer = 0.15f;


  public override void EnterState(PlayerController entity)
  {
    Debug.Log("PlayerHurtState EnterState");
    timer = entity.knockbackDuration;
    //entity.AnimatorBridge.TriggerHurt();
  }


  public override void UpdateState(PlayerController entity)
  {
    // timer -= Time.deltaTime;
    // if (timer <= 0f)
    // {
    //   entity.playerSM.ChangeState(new PlayerIdleState());
    // }
  }
}
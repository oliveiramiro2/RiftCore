using UnityEngine;


public class PlayerDeathState : State<PlayerController>
{
  public override void EnterState(PlayerController entity)
  {
    base.EnterState(entity);
    // Play death animation, disable inputs, etc.
    //entity.AnimatorBridge.TriggerHurt();
  }
}
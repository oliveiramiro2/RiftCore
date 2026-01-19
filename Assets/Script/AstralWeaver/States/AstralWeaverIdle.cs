using UnityEngine;

public class AstralWeaverIdle : State<AstralWeaverController>
{
  private float teleportTimer = 0.5f;
  private bool hasTeleported = false, animationIn = false, animationOut = true, toIdle = false, isShieldActive = false;

  public override void EnterState(AstralWeaverController entity)
  {
    hasTeleported = false;
    teleportTimer = 0.5f;
    animationIn = false;
    animationOut = true;
    AstralWeaverDamageHandler damageControl = entity.GetComponent<AstralWeaverDamageHandler>();
    isShieldActive = damageControl.shieldIsActive;
    if (!damageControl.shieldIsActive)
      entity.AnimatorBridge.AstralWeaverIdle();
  }

  public override void UpdateState(AstralWeaverController entity)
  {
    if (isShieldActive) return;
    if (!hasTeleported)
    {
      if (animationIn)
      {
        entity.awEvents.OnTeleportIn.Raise();
        entity.AnimatorBridge.AstralWeaverTeleportIn();
        animationIn = false;
        hasTeleported = true;
        toIdle = true;
        teleportTimer = 0.3f;
      }
      else if (animationOut)
      {
        entity.awEvents.OnTeleportOut.Raise();
        entity.AnimatorBridge.AstralWeaverTeleport();
        animationOut = false;
      }

      teleportTimer -= Time.deltaTime;

      if (teleportTimer <= 0f)
      {
        entity.LocomotionModule.TeleportToRandomPoint(entity);
        animationIn = true;
      }
    }
    if (toIdle)
    {
      teleportTimer -= Time.deltaTime;
      if (teleportTimer <= 0f)
      {
        entity.AnimatorBridge.AstralWeaverIdle();
        toIdle = false;
      }
    }
  }

  public override void ExitState(AstralWeaverController entity)
  {
  }
}

using UnityEngine;

public class LasterEvents : MonoBehaviour
{
  private LasterController controller;

  void Start()
  {
    controller = GameObject.FindAnyObjectByType<LasterController>();
  }


  public void LasterTeleportOut()
  {
    controller.events.TeleportOut.Raise();
  }

  public void LasterTeleportIn()
  {
    controller.events.TeleportIn.Raise();
  }

  public void LasterSlashAttack()
  {
    controller.events.SlashAttack.Raise();
  }

  public void LasterLaserStart()
  {
    controller.events.LaserStart.Raise();
  }

  public void LasterLaserFinish()
  {
    controller.events.LaserFinish.Raise();
  }

  public void LasterCastGreatBall()
  {
    controller.events.CastGreatBall.Raise();
  }
}
using UnityEngine;

public class LasterEvents : MonoBehaviour
{
  private LasterController controller;

  void Start()
  {
    controller = GameObject.FindAnyObjectByType<LasterController>();
  }


  public void MawTeleportOut()
  {
    controller.events.TeleportOut.Raise();
  }

  public void MawTeleportIn()
  {
    controller.events.TeleportIn.Raise();
  }

  public void MawSlashAttack()
  {
    controller.events.SlashAttack.Raise();
  }
}
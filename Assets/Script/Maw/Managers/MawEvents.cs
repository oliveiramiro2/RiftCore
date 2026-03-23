using UnityEngine;

public class MawEvents : MonoBehaviour
{
  private MawController controller;

  void Start()
  {
    controller = GameObject.FindAnyObjectByType<MawController>();
  }

  public void MawExplosionEvent()
  {
    controller.events.Explosion.Raise();
  }

  public void MawSummonStaffEvent()
  {
    controller.events.SummonStaff.Raise();
  }

  public void MawStaffHitFloorEvent()
  {
    controller.events.StaffHitFloor.Raise();
  }



  public void MawFloatingEvent()
  {
    controller.events.Floating.Raise();
  }

  public void MawTeleport()
  {
    controller.events.Teleport.Raise();
  }

  public void MawTeleport2()
  {
    controller.events.Teleport2.Raise();
  }

  public void MawStartSummon()
  {
    controller.events.StartSummon.Raise();
  }

  public void MawZombieStart()
  {
    controller.events.ZombieStart.Raise();
  }
}
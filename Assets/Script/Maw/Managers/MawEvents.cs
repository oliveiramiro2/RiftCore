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
}
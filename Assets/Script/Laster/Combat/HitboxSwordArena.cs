using UnityEngine;

public class HitboxSwordArena : MonoBehaviour
{

  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent(out Hurtbox hurtbox))
    {
      GameObject.FindAnyObjectByType<LasterEventsManager>().SwordArenaHit.Raise();
    }
  }
}
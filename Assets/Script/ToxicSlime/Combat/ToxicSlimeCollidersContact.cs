using UnityEngine;

public class ToxicSlimeCollidersContact : MonoBehaviour
{
  [SerializeField] private Hitbox slapHitbox;

  public void EnableSlapHitbox()
  {
    slapHitbox.Activate();
  }

  public void DisableSlapHitbox()
  {
    slapHitbox.Deactivate();
  }
}
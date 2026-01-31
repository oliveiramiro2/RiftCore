using UnityEngine;

public class ToxicSlimeCollidersContact : MonoBehaviour
{
  [SerializeField] private Hitbox slapHitbox;
  [SerializeField] private Hitbox rollHitbox;

  public void EnableSlapHitbox()
  {
    slapHitbox.Activate();
  }

  public void DisableSlapHitbox()
  {
    slapHitbox.Deactivate();
  }

  public void EnableRollHitbox()
  {
    rollHitbox.Activate();
  }

  public void DisableRollHitbox()
  {
    rollHitbox.Deactivate();
  }
}
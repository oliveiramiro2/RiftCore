using UnityEngine;

public class MawColliders : MonoBehaviour
{
  [SerializeField] private CircleCollider2D explosionCollider;
  [SerializeField] private BoxCollider2D contactCollider;

  public void EnableExplosionCollider()
  {
    explosionCollider.enabled = true;
  }

  public void DisableExplosionCollider()
  {
    explosionCollider.enabled = false;
  }
  public void DisableContactCollider()
  {
    contactCollider.enabled = false;
  }
}
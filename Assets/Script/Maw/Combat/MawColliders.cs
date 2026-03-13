using UnityEngine;

public class MawColliders : MonoBehaviour
{
  [SerializeField] private CircleCollider2D explosionCollider;

  public void EnableExplosionCollider()
  {
    explosionCollider.enabled = true;
  }

  public void DisableExplosionCollider()
  {
    explosionCollider.enabled = false;
  }
}
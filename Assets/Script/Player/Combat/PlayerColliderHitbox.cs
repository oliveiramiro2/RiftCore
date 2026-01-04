using UnityEngine;

public class PlayerColliderHitbox : MonoBehaviour
{

  public int damage = 1;
  public float knockbackForce = 5f;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (TryGetComponent<IDamageable>(out var target))
    {
      Vector2 direction = (other.transform.position - transform.position).normalized;
      target.TakeDamage(damage, direction, knockbackForce);
    }
  }
}
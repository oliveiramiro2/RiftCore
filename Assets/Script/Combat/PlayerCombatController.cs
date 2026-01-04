using UnityEngine;


[DisallowMultipleComponent]
public class PlayerCombatController : MonoBehaviour
{
  public Transform slashHitboxTransform;
  public Collider2D slashHitboxCollider;
  public int damage = 10;


  private void Awake()
  {
    if (slashHitboxCollider != null)
      slashHitboxCollider.enabled = false;
  }


  public void EnableHitbox(bool v)
  {
    if (slashHitboxCollider != null)
      slashHitboxCollider.enabled = v;
  }


  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!slashHitboxCollider || !slashHitboxCollider.enabled) return;


    if (other.CompareTag("Enemy"))
    {
      var health = other.GetComponent<IDamageable>();
      if (health != null)
      {
        health.TakeDamage(damage, Vector2.up, 1);
      }
    }
  }
}
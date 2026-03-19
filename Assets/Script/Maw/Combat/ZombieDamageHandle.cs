using UnityEngine;

public class ZombieDamageHandle : DamageHandlerBase<Zombie>
{
  private bool phase1 = true;

  protected override void Awake()
  {
    base.Awake();
  }

  void OnEnable()
  {
    base.currentHealth = 50;
  }

  public override void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce)
  {
    base.TakeDamage(damage, hitDirection, knockbackForce);

    if (isDead) return;
  }

  protected override void Die()
  {
    gameObject.SetActive(false);
  }
}
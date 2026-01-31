using UnityEngine;

public class ToxicSlimeDamageHandler : DamageHandlerBase<ToxicSlimeController>
{
  private bool phase1 = true;

  protected override void Awake()
  {
    base.Awake();
  }

  public override void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce)
  {
    base.TakeDamage(damage, hitDirection, knockbackForce);

    if (isDead) return;

    if (controller.Phase2() && phase1)
    {
      phase1 = false;
    }
  }

  protected override void Die()
  {
    base.Die();
    isDead = true;
    controller.MarkAsDead();

    Destroy(gameObject, 3f);
  }
}
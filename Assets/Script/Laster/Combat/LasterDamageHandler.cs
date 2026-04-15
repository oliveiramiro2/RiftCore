using UnityEngine;

public class LasterDamageHandler : DamageHandlerBase<LasterController>
{
  private bool phase1 = true;

  protected override void Awake()
  {
    base.Awake();
  }

  public override void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce)
  {
    controller.events.Hurt.Raise();
    base.TakeDamage(damage, hitDirection, knockbackForce);

    if (isDead) return;
    if (controller.Phase2() && phase1)
    {
      phase1 = false;
      controller.events.Phase2.Raise();
    }
  }

  protected override void Die()
  {
    controller.MarkAsDead();
    base.Die();
    controller.events.Death.Raise();
    controller.AnimatorBridge.LasterDeath();
    isDead = true;
    Destroy(gameObject, 5f);
  }
}
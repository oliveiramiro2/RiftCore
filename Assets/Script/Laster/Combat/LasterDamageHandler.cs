using UnityEngine;

public class LasterDamageHandler : DamageHandlerBase<LasterController>
{
  private bool phase1 = true;
  private bool was25 = false, was75 = false;

  protected override void Awake()
  {
    base.Awake();
  }

  public override void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce)
  {
    controller.events.Hurt.Raise();
    base.TakeDamage(damage, hitDirection, knockbackForce);

    if (isDead) return;
    if (!was25 && controller.currentHealth <= controller.maxHealth * 0.25f)
    {
      was25 = true;
      controller.isAttacking = false;
      controller.events.Stuned.Raise();
    }
    else if (!was75 && controller.currentHealth <= controller.maxHealth * 0.75f)
    {
      was75 = true;
      controller.isAttacking = false;
      controller.events.Stuned.Raise();
    }
    if (controller.Phase2() && phase1)
    {
      phase1 = false;
      controller.events.Phase2.Raise();
      controller.isAttacking = false;
      controller.AnimatorBridge.StartTeleportTime(2f);
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
using UnityEngine;

public class ToxicSlimeDamageHandler : DamageHandlerBase<ToxicSlimeController>
{
  private bool phase1 = true;
  private ToxicSlimeController owner;

  protected override void Awake()
  {
    base.Awake();
    owner = gameObject.GetComponentInParent<ToxicSlimeController>();
  }

  public override void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce)
  {
    base.TakeDamage(damage, hitDirection, knockbackForce);
    controller.tsEvents.OnToxicHurt.Raise();

    if (isDead) return;

    if (controller.Phase2() && phase1)
    {
      phase1 = false;
      controller.tsEvents.OnToxicPhase2.Raise();
    }
  }

  protected override void Die()
  {
    base.Die();
    isDead = true;
    controller.tsEvents.OnToxicDeath.Raise();
    controller.MarkAsDead();
    Destroy(gameObject, 3f);
  }
}
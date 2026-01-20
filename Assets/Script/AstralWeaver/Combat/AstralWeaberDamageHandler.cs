using UnityEngine;

public class AstralWeaverDamageHandler : DamageHandlerBase<AstralWeaverController>
{
  private bool phase1 = true;
  public bool shieldIsActive = false;

  protected override void Awake()
  {
    base.Awake();
  }

  public override void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce)
  {
    if (shieldIsActive) damage = Mathf.FloorToInt(damage / 2);
    base.TakeDamage(damage, hitDirection, knockbackForce);

    if (isDead) return;
    controller.awEvents.OnHurt.Raise();

    if (controller.Phase2() && phase1)
    {
      phase1 = false;
      controller.awEvents.OnPhase2.Raise();
    }
  }

  protected override void Die()
  {
    base.Die();
    isDead = true;
    controller.awEvents.OnDeath.Raise();
  }
}
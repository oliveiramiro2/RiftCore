using UnityEngine;

public class MawDamageHandler : DamageHandlerBase<MawController>
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
      //controller.Events.Phase2.Raise();
    }
  }

  protected override void Die()
  {
    controller.MarkAsDead();
    base.Die();
    //controller.Events.Death.Raise();
    isDead = true;
    Destroy(gameObject, 3f);
  }
}
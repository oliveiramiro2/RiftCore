using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxV2 : MonoBehaviour
{
  public AttackData data;

  private IAttacker attacker;
  private Collider2D col;

  public void Init(IAttacker newAttacker)
  {
    attacker = newAttacker;
  }

  void Awake()
  {
    col = GetComponent<Collider2D>();
    col.isTrigger = true;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (attacker == null)
    {
      Debug.LogError("Hitbox sem attacker!");
      return;
    }

    if (other.gameObject == attacker.GetOwner())
      return;

    if (other.TryGetComponent(out Hurtbox hurtbox))
    {
      int finalDamage = data.baseDamage * attacker.GetDamageMultiplier();

      Vector2 dir = (other.transform.position - transform.position).normalized;

      hurtbox.ApplyDamage(finalDamage, dir, data.knockbackForce);
    }
  }
}
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public IDamageable owner;

    private void Awake()
    {
        owner = GetComponentInParent<IDamageable>();
    }

    public void ApplyDamage(int damage, Vector2 direction, float knockbackForce)
    {
        owner?.TakeDamage(damage, direction, knockbackForce);
    }
}

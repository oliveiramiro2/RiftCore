using UnityEngine;

public class HitBoxToInstaAttack : MonoBehaviour
{
    [Header("Damage Info")]
    public int damage = 1;
    public float knockbackForce = 0.1f;
    public Vector2 direction = Vector2.right;
    public bool startHitboxDeactivate;

    [Header("Owner")]
    public GameObject owner;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (owner == null) return;
        if (other.gameObject == owner) return;

        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            Vector2 hitDir = (other.transform.position - owner.transform.position).normalized;
            hurtbox.ApplyDamage(damage, hitDir, knockbackForce);
        }
    }
}

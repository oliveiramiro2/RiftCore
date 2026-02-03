using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
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

        if (startHitboxDeactivate)
            Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (owner == null) return;
        if (other.gameObject == owner) return;
        if (other.CompareTag("DestroyBullets") && gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject, 0.05f);
        }

        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            int finalDamage = damage;
            if (gameObject.CompareTag("SwordHitbox"))
            {
                int player = owner.GetComponent<PlayerController>().buffSwordDamage;
                finalDamage *= player;
                owner.GetComponent<PlayerController>().events.OnPlayerHitEnemy.Raise();

            }

            Vector2 hitDir = (other.transform.position - owner.transform.position).normalized;
            hurtbox.ApplyDamage(finalDamage, hitDir, knockbackForce);
        }
    }

    // Chamado por Animation Event
    public void Activate() => gameObject.GetComponent<Collider2D>().enabled = true;
    public void Deactivate() => gameObject.GetComponent<Collider2D>().enabled = false;

}

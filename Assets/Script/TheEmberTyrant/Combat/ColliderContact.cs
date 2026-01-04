using UnityEngine;

public class ColliderContact : MonoBehaviour
{
    public int damage = 1;
    public float knockbackforce = 20f;
    public Collider2D explosionCollider;
    public Collider2D punchCollider;

    private BossController controller;

    private void Awake()
    {
        controller = GetComponentInParent<BossController>();
    }

    public void EnablePunch()
    {
        if (!controller.IsDead)
            punchCollider.enabled = true;
    }

    public void DisablePunch()
    {
        punchCollider.enabled = false;
    }

    public void EnableExplosion()
    {
        if (!controller.IsDead)
            explosionCollider.enabled = true;
    }

    public void DisableExplosion()
    {
        explosionCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Hurtbox>(out var target) && !controller.IsDead)
        {
            Vector2 direction = (other.transform.position - transform.position).normalized;
            target.ApplyDamage(damage, direction, knockbackforce);
            controller.BossPhysics.TETStopHorizontal();
        }
    }
}

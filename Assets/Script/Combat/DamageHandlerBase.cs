using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class DamageHandlerBase<T> : MonoBehaviour, IDamageable where T : BaseEntity
{
    [Header("Health Settings")]
    public int maxHealth = 12;
    public int currentHealth;
    public int upgradeLifeBonus = 1;

    [Header("Invincibility Settings")]
    public float invincibilityDuration = 0.5f;
    protected float invincibilityTimer = 0f;

    [HideInInspector] public bool isHurt;
    [HideInInspector] public bool isDead;

    protected Rigidbody2D rb;
    protected T controller;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<T>();

        currentHealth = maxHealth;

        controller.maxHealth = maxHealth;
        controller.currentHealth = currentHealth;
    }

    protected virtual void Update()
    {
        if (invincibilityTimer > 0f)
            invincibilityTimer -= Time.deltaTime;

        if (isHurt)
        {
            controller.knockbackTimer -= Time.deltaTime;
            if (controller.knockbackTimer <= 0f)
                isHurt = false;
        }
        if (isDead)
        {
            controller.MarkAsDead();
        }
    }

    public void EnableInvincibility()
    {
        invincibilityTimer = invincibilityDuration;
    }

    public virtual void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce = 4f)
    {
        if (isDead || invincibilityTimer > 0f)
            return;

        currentHealth -= damage;
        controller.currentHealth = currentHealth;

        ApplyKnockback(hitDirection, knockbackForce);

        EnableInvincibility();

        isHurt = true;
        controller.knockbackTimer = controller.knockbackDuration;

        if (currentHealth <= 0)
            Die();
    }

    private void ApplyKnockback(Vector2 dir, float force)
    {
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = new Vector2(dir.x * force, rb.linearVelocity.y + dir.y * force * 0.5f);
    }

    protected virtual void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
    }
}

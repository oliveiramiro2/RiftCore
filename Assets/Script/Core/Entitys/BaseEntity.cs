using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseEntity : MonoBehaviour
{
    [Header("Common Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool canMove = true;


    [Header("Combat")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public bool IsDead { get; protected set; }


    [Header("Knockback Settings")]
    public float knockbackDuration = 0.05f;
    public float knockbackTimer;

    [Header("Debuff's")]
    public float slowVelocity = 1, durationSlow;
    protected bool isSlowed = false;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void MarkAsDead()
    {
        IsDead = true;
    }

    public void ApplySlow(float duration, float intensity = 1f)
    {
        if (!isSlowed)
        {
            durationSlow = duration;
            slowVelocity += intensity;
            isSlowed = true;
            Debug.Log($"duration: {durationSlow} / slow: {slowVelocity} / bool: {isSlowed}");
            return;
        }
        durationSlow += duration;
        slowVelocity += intensity;
        Debug.Log($"ja tem duration: {durationSlow} / slow: {slowVelocity} / bool: {isSlowed}");
    }
}
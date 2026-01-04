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


    [Header("Combat")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public bool IsDead { get; protected set; }


    [Header("Knockback Settings")]
    public float knockbackDuration = 0.15f;
    public float knockbackTimer;


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
}
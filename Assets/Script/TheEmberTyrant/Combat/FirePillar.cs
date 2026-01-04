using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class FirePillar : MonoBehaviour
{
    [Header("references")]
    private Animator animator;
    private Collider2D col;

    [Header("Fire Pillar Settings")]
    public float knockbackForce = 10f;
    public int damage = 1;

    public float timerGrow = 1.2f;
    public float lifeTime = 5f;
    private float currentTimeGrow = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        currentTimeGrow += Time.deltaTime;
        if (currentTimeGrow >= timerGrow)
        {
            animator.Play("firepillar");
            col.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Hurtbox>(out var target))
        {
            Vector2 hitDir = (other.transform.position - transform.position).normalized;
            target.ApplyDamage(damage, hitDir, knockbackForce); // chama o dano
        }
    }
}

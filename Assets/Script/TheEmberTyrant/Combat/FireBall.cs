using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 6f;
    public float lifetime = 5f;
    public int damage = 1;
    public float knockbackForce = 30f;

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Hurtbox>(out var target))
        {
            Vector2 hitDir = (other.transform.position - transform.position).normalized;
            target.ApplyDamage(damage, hitDir, knockbackForce); // chama o dano
        }
        Destroy(gameObject);
    }

}

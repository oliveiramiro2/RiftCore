using UnityEngine;

public class SwordArena : MonoBehaviour
{
    public float speed = 5f;
    private readonly int direction = 1;
    private readonly float lifeTime = 5f;

    void Start()
    {
        speed += Random.Range(-1f, 1f);
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime * Vector2.right);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Hurtbox hurtbox))
        {
            GameObject.FindAnyObjectByType<LasterEvents>().SwordArenaHitEvent();
        }
    }
}

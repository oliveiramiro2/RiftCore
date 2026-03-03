using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StormPull : MonoBehaviour
{
  [Header("Pull Settings")]
  [SerializeField] private float pullForce = 15f;
  [SerializeField] private float radius = 3f;
  [SerializeField] private float duration = 2f;

  private float timer;

  private void OnEnable()
  {
    timer += duration;
  }

  private void Update()
  {
    timer -= Time.deltaTime;

    if (timer <= 0f)
    {
      gameObject.SetActive(false);
      timer = 0f;
    }
  }

  private void FixedUpdate()
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

    foreach (Collider2D hit in hits)
    {
      if (!hit.CompareTag("Player")) continue;

      Rigidbody2D rb = hit.attachedRigidbody;
      if (rb == null) continue;

      Vector2 direction = (transform.position - hit.transform.position).normalized;

      float distance = Vector2.Distance(transform.position, hit.transform.position);

      float forceMultiplier = 1f - (distance / radius);

      rb.AddForce(direction * pullForce * forceMultiplier, ForceMode2D.Force);
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
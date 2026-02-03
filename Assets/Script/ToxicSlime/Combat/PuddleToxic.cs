using UnityEngine;

public class PuddleToxic : MonoBehaviour
{
  private float fallSpeed = 1f, timer = 0, duration = 3f;
  private ParticleSystem explosion;

  private void Start()
  {
    explosion = gameObject.GetComponentInChildren<ParticleSystem>();
  }

  void Update()
  {
    timer += Time.deltaTime;

    if (timer > duration)
      transform.position += Vector3.down * fallSpeed * Time.deltaTime;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      explosion.Play();

      Destroy(gameObject, 0.1f);
    }
  }
}
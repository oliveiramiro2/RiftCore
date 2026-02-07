using UnityEngine;

public class PuddleToxic : MonoBehaviour
{
  private float fallSpeed = 1f, timer = 0, duration = 5f;
  private ParticleSystem explosion;
  private ToxicSlimeController owner;

  private void Start()
  {
    explosion = gameObject.GetComponentInChildren<ParticleSystem>();
    owner = FindAnyObjectByType<ToxicSlimeController>();
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
      owner.tsEvents.OnToxicProjectilExplosion.Raise();
      Destroy(gameObject, 0.1f);
    }
  }
}
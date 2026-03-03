using UnityEngine;

public class AirSlashSM : MonoBehaviour
{
  private readonly int speed = 18;
  private readonly float lifeTime = 10f;
  private float timer = 0f;

  void Update()
  {
    timer += Time.deltaTime;
    if (timer >= lifeTime)
    {
      gameObject.SetActive(false);
      timer = 0f;
    }
    transform.Translate(Vector2.down * speed * Time.deltaTime);
  }
}
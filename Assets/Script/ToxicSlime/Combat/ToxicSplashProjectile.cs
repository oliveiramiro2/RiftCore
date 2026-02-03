using UnityEngine;

public class ToxicSplashProjectile : MonoBehaviour
{
  private Vector2 startPos;
  private Vector2 targetPos;

  private float travelTime;
  private float arcHeight;
  private float timer;

  private bool wasRotated = false;

  public void Launch(Vector2 target, float time, float height)
  {
    startPos = transform.position;
    targetPos = target;
    travelTime = time;
    arcHeight = height;
    timer = 0f;
  }

  void Update()
  {
    if (timer >= 0) gameObject.GetComponent<Rigidbody2D>().IsSleeping();

    timer += Time.deltaTime;
    float t = timer / travelTime;

    if (startPos == Vector2.zero || targetPos == Vector2.zero) return;

    Vector2 pos = Vector2.Lerp(startPos, targetPos, t);
    float arc = arcHeight * 4f * (t - t * t);

    transform.position = pos + Vector2.up * arc;
    if (!wasRotated && gameObject.GetComponent<Rigidbody2D>().linearVelocityY < 0)
    {
      transform.localScale = new Vector3(1, -1, 1);
      wasRotated = true;
    }
  }
}

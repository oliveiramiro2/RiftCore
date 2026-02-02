using UnityEngine;

public class ToxicSplashProjectile : MonoBehaviour
{
  Vector2 startPos;
  Vector2 targetPos;

  float travelTime;
  float arcHeight;
  float timer;

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

    timer += Time.deltaTime;
    float t = timer / travelTime;

    if (startPos == Vector2.zero || targetPos == Vector2.zero) return;

    Vector2 pos = Vector2.Lerp(startPos, targetPos, t);
    float arc = arcHeight * 4f * (t - t * t);

    transform.position = pos + Vector2.up * arc;
  }
}

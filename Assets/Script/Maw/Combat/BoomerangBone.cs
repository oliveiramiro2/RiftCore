using UnityEngine;

public class BoomerangBone : MonoBehaviour
{
  public float speed = 12f;
  public float rotationSpeed = 720f;

  public float lifeTime = 6f;

  float timer;
  int direction = 1;

  void OnEnable()
  {
    timer = 0f;
  }

  void Update()
  {
    timer += Time.deltaTime;

    RotateBone();
    MoveBone();

    if (timer >= lifeTime)
    {
      gameObject.SetActive(false);
    }
  }

  void RotateBone()
  {
    transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
  }

  void MoveBone()
  {
    float halfTime = lifeTime * 0.5f;

    if (timer < halfTime)
    {
      transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
    }
    else
    {

      transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
    }
  }
}
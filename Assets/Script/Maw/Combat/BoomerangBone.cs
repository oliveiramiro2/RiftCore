using UnityEngine;

public class BoomerangBone : MonoBehaviour
{
  public float speed = 12f;
  public float rotationSpeed = 720f;

  public float lifeTime = 6f;

  private Transform target, initPos;

  float timer;
  int direction = 1;

  void Awake()
  {
    target = GameObject.FindAnyObjectByType<PlayerController>().transform;
    initPos = GameObject.FindAnyObjectByType<MawController>().transform;
  }

  void OnEnable()
  {
    timer = 0f;
    if (target.position.x > initPos.position.x)
    {
      direction = -1;
    }
    else
    {
      direction = 1;
    }
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
      transform.Translate(direction * speed * Time.deltaTime * Vector2.left, Space.World);
    }
    else
    {
      transform.Translate(direction * speed * Time.deltaTime * Vector2.right, Space.World);
    }
  }
}
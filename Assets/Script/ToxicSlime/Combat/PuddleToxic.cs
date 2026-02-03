using UnityEngine;

public class PuddleToxic : MonoBehaviour
{
  private float fallSpeed = 1f, timer = 0, duration = 2f;

  void Update()
  {
    timer += Time.deltaTime;

    if (timer > duration)
      transform.position += Vector3.down * fallSpeed * Time.deltaTime;
  }


}
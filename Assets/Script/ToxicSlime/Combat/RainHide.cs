using UnityEngine;

public class RainHide : MonoBehaviour
{
  private GameObject rain;

  private void Awake()
  {
    rain = this.gameObject;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      Destroy(rain, 0.1f);
    }
  }
}
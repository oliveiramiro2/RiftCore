using UnityEngine;

public class RainHide : MonoBehaviour
{
  private GameObject rain;

  private void Awake()
  {
    rain = this.gameObject;
  }

  private void Start()
  {
    gameObject.transform.localScale = new Vector3(1, -1, 1);
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      Destroy(rain, 0.1f);
    }
  }
}
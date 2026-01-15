using UnityEngine;

public class EnergyBallHiding : MonoBehaviour
{
  [SerializeField] private GameObject EnergyBall;
  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      EnergyBall.SetActive(false);
    }
  }
}
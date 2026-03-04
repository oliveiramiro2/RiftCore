using UnityEngine;

public class WindReactive : MonoBehaviour
{
  public float maxAngle = 10f;
  public float swaySpeedMultiplier = 1f;

  private float randomOffset;

  private void Start()
  {
    randomOffset = Random.Range(0f, 100f);
  }

  void Update()
  {
    if (WindController.Instance == null) return;

    float strength = WindController.Instance.windStrength;
    float speed = WindController.Instance.windSpeed;

    float sway = Mathf.Sin((Time.time * speed * swaySpeedMultiplier) + randomOffset);

    float angle = sway * maxAngle * strength;

    transform.localRotation = Quaternion.Euler(0f, 0f, angle);
  }
}
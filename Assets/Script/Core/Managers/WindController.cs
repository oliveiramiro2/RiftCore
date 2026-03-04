using UnityEngine;

public class WindController : MonoBehaviour
{
  public static WindController Instance;

  [Range(0f, 2f)]
  public float windStrength = 1f;

  public float windSpeed = 1f;

  private void Awake()
  {
    Instance = this;
  }
}
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
  public Image fillImage;
  public Material heartMaterial;

  public void SetFill(float value)
  {
    fillImage.fillAmount = value;

    if (value < 1f)
      TriggerPulse();
  }

  void TriggerPulse()
  {
    heartMaterial.SetFloat("_PulseBoost", 0.5f);
  }
}
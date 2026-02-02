using UnityEngine;

public class ToxicSlimeFXManager : MonoBehaviour
{
  public ParticleSystem ToxicRainPrepairEffect;
  public ParticleSystem ToxicRainCloudEffect;

  public void PlayToxicRainPrepairEffect()
  {
    ToxicRainPrepairEffect.Play();
  }

  public void PlayToxicRainCloudEffect()
  {
    ToxicRainCloudEffect.Play();
  }
}
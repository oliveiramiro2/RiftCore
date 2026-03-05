using UnityEngine;
using System.Collections;

public class SMGameFlowManager : MonoBehaviour
{

  public void OnBossEffectTime()
  {
    StartCoroutine(PlayEffects());
  }

  private IEnumerator PlayEffects()
  {
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(1f);
    Time.timeScale = 1f;
  }
}
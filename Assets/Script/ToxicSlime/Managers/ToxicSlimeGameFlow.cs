using UnityEngine;
using System.Collections;

public class ToxicSlimeGameFlow : MonoBehaviour
{
  public void OnBossPhase2()
  {
    StartCoroutine(PlayPhase2Effects());
  }


  private IEnumerator PlayPhase2Effects()
  {
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(0.5f);
    Time.timeScale = 1f;
  }
}
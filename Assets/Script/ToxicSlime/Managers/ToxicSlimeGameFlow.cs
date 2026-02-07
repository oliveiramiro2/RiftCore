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
    ToxicSlimeController ts = GameObject.FindAnyObjectByType<ToxicSlimeController>();
    ts.canMove = false;
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(0.1f);
    Time.timeScale = 1f;
    yield return new WaitForSeconds(2f);
    ts.canMove = true;
  }
}
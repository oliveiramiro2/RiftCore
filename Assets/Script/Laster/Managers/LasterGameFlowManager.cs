using UnityEngine;
using System.Collections;

public class LasterGameFlowManager : MonoBehaviour
{
  private LasterController controller;

  void Start()
  {
    controller = GameObject.FindAnyObjectByType<LasterController>();
  }

  public void OnBossEffectTime()
  {
    StartCoroutine(PlayEffects());
  }

  private IEnumerator PlayEffects()
  {
    controller.canMove = false;
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(1f);
    Time.timeScale = 1f;
    controller.canMove = true;
  }
}
using UnityEngine;
using System.Collections;

public class MawGameFlowManager : MonoBehaviour
{
  private MawController controller;

  void Start()
  {
    controller = GameObject.FindAnyObjectByType<MawController>();
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
using UnityEngine;
using System.Collections;

public class AWGameFlowManager : MonoBehaviour
{
  [SerializeField] private ParticleSystem playerHitSparksLeft;
  [SerializeField] private ParticleSystem playerHitSparksRight;


  public void ShieldIsActiveEffect()
  {
    var mainLeft = playerHitSparksLeft.main;
    mainLeft.startColor = Color.cyan;
    var mainRight = playerHitSparksRight.main;
    mainRight.startColor = Color.cyan;
  }

  public void SwordBuffEffect()
  {
    var mainLeft = playerHitSparksLeft.main;
    mainLeft.startColor = Color.red;
    var mainRight = playerHitSparksRight.main;
    mainRight.startColor = Color.red;
  }

  public void ResetPlayerHitSparksEffect()
  {
    var mainLeft = playerHitSparksLeft.main;
    mainLeft.startColor = Color.white;
    var mainRight = playerHitSparksRight.main;
    mainRight.startColor = Color.white;
  }

  public void OnBossPhase2()
  {
    StartCoroutine(PlayPhase2Effects());
  }

  public void OnBossDeath()
  {
    StartCoroutine(PlayDeathEffects());
  }

  private IEnumerator PlayPhase2Effects()
  {
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(0.5f);
    Time.timeScale = 1f;
  }

  private IEnumerator PlayDeathEffects()
  {
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(1f);
    Time.timeScale = 1f;
  }
}
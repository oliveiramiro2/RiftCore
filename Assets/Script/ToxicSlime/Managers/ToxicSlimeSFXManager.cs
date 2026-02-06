using UnityEngine;

public class ToxicSlimeSFXManager : MonoBehaviour
{
  public SoundData death;
  public SoundData projectilExplosion;
  public SoundData hurt;
  public SoundData jumpImpact;
  public SoundData phase2;
  public SoundData projectilLand;
  public SoundData rainFall;
  public SoundData regular;
  public SoundData vomit;
  public SoundData rolling;
  public SoundData slap;

  public void PlayDeath()
  {
    death.Play();
  }
  public void PlayProjectilExplosion()
  {
    projectilExplosion.Play();
  }

  public void PlayHurt()
  {
    hurt.Play();
  }

  public void PlayJumpImpact()
  {
    jumpImpact.Play();
  }

  public void PlayPhase2()
  {
    phase2.Play();
  }

  public void PlayProjectilLand()
  {
    projectilLand.Play();
  }

  public void PlayRainFall()
  {
    rainFall.Play();
  }

  public void PlayRegular()
  {
    regular.Play();
  }

  public void PlayVomit()
  {
    vomit.Play();
  }

  public void PlayRolling()
  {
    rolling.Play();
  }

  public void PlaySlap()
  {
    slap.Play();
  }
}
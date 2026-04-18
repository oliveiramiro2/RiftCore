using UnityEngine;

public class LasterSFXManager : MonoBehaviour
{
  public SoundData teleporOut;
  public SoundData teleportIn;
  public SoundData slashAttack;
  public SoundData laserStart;
  public SoundData laserFinish;
  public SoundData laserHit;
  public SoundData castGreatBall;
  public SoundData swordArenaHit;


  public void PlayTeleportOut()
  {
    teleporOut.Play();
  }

  public void PlayTeleportIn()
  {
    teleportIn.Play();
  }

  public void PlaySlashAttack()
  {
    slashAttack.Play();
  }

  public void PlayLaserStart()
  {
    laserStart.Play();
  }

  public void PlayLaserFinish()
  {
    laserFinish.Play();
  }

  public void PlayLaserHit()
  {
    laserHit.Play();
  }

  public void PlayCastGreatBall()
  {
    castGreatBall.Play();
  }

  public void PlaySwordArenaHit()
  {
    swordArenaHit.Play();
  }
}
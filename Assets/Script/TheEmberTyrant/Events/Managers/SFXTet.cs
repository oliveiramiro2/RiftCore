using UnityEngine;

public class SFXTet : MonoBehaviour
{
  public SoundData punchSound;
  public SoundData fireBallSound;
  public SoundData firePilarSound;
  public SoundData dashSound;
  public SoundData explosionSound;
  public SoundData roarSound;
  public SoundData deathSound;

  public void PlayPunchSound()
  {
    punchSound.Play();
  }

  public void PlayFireBallSound()
  {
    fireBallSound.Play();
  }

  public void PlayFirePilarSound()
  {
    firePilarSound.Play();
  }

  public void PlayDashSound()
  {
    dashSound.Play();
  }

  public void PlayDeathSound()
  {
    deathSound.Play();
  }

  public void PlayExplosionSound()
  {
    explosionSound.Play();
  }

  public void PlayRoarSound()
  {
    roarSound.Play();
  }
}

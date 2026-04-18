using UnityEngine;

public class LasterSFXManager : MonoBehaviour
{
  public SoundData teleporOut;
  public SoundData teleportIn;
  public SoundData slashAttack;


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
}
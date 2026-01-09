using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [Header("Attack")]
    public SoundData attackSound;

    [Header("Movement")]
    public SoundData moveSound;
    public SoundData landSound;
    public SoundData jumpSound;
    public SoundData dashSound;
    public SoundData hitSound;
    public SoundData deathSound;


    public void PlaySlashSound()
    {
        attackSound.Play();
    }

    public void PlayMoveSound()
    {
        moveSound.Play();
    }
    public void PlayLandSound()
    {
        landSound.Play();
    }
    public void PlayJumpSound()
    {
        jumpSound.Play();
    }
    public void PlayDashSound()
    {
        dashSound.Play();
    }

    public void PlayHitSound()
    {
        hitSound.Play();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }
}

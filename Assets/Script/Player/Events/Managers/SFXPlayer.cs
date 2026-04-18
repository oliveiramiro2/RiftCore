using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [Header("SFX")]
    public SoundData attackSound;
    public SoundData moveSound;
    public SoundData landSound;
    public SoundData jumpSound;
    public SoundData dashSound;
    public SoundData hitSound;
    public SoundData deathSound;
    public SoundData LaserAttack;
    public SoundData PlayerHitingEnemy;


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

    public void PlayLaserSound()
    {
        LaserAttack.Play();
    }

    public void PlayPlayerHitEnemySound()
    {
        PlayerHitingEnemy.Play();
    }
}

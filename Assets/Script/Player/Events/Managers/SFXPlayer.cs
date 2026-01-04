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


    public void PlaySlashSound()
    {
        // --- Toca som de ataque ---
        attackSound.Play();
    }

    public void PlayMoveSound()
    {
        // --- Toca som de ataque ---
        moveSound.Play();
    }
    public void PlayLandSound()
    {
        // --- Toca som de ataque ---
        landSound.Play();
    }
    public void PlayJumpSound()
    {
        // --- Toca som de ataque ---
        jumpSound.Play();
    }
    public void PlayDashSound()
    {
        // --- Toca som de ataque ---
        dashSound.Play();
    }


}

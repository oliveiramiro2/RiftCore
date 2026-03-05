using UnityEngine;

public class SFXSMManager : MonoBehaviour
{
    public SoundData slash1;
    public SoundData slash2;
    public SoundData slash3;
    public SoundData explosion;
    public SoundData footstep;
    public SoundData laughter;
    public SoundData airSlash;
    public SoundData Storm;
    public SoundData teleport;
    public SoundData parry;
    public SoundData counterAttack;
    public SoundData breathing;
    public SoundData death;
    public SoundData phase2;

    public void PlaySlash1()
    {
        slash1.Play();
    }

    public void PlaySlash2()
    {
        slash2.Play();
    }

    public void PlaySlash3()
    {
        slash3.Play();
    }

    public void PlayExplosion()
    {
        explosion.Play();
    }

    public void PlayFootstep()
    {
        footstep.Play();
    }
    public void PlayLaughter()
    {
        laughter.Play();
    }

    public void PlayAirSlash()
    {
        airSlash.Play();
    }

    public void PlayStorm()
    {
        Storm.Play();
    }

    public void PlayTeleport()
    {
        teleport.Play();
    }

    public void PlayParry()
    {
        parry.Play();
    }

    public void PlayCounterAttack()
    {
        counterAttack.Play();
    }

    public void PlayBreathing()
    {
        breathing.Play();
    }

    public void PlayDeath()
    {
        death.Play();
    }

    public void PlayPhase2()
    {
        phase2.Play();
    }
}

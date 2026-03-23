using UnityEngine;

public class MawSFXManager : MonoBehaviour
{
    public SoundData explosion;
    public SoundData staff;
    public SoundData staffHitFloot;
    public SoundData floating;
    public SoundData startSummon;
    public SoundData teleport;
    public SoundData teleport2;
    public SoundData zombieStart;
    public SoundData death;

    public void PlayExplosion()
    {
        explosion.Play();
    }

    public void PlayStaffEffect()
    {
        staff.Play();
    }

    public void PlayStaffHitFloor()
    {
        staffHitFloot.Play();
    }

    public void PlayFloating()
    {
        floating.Play();
    }

    public void PlayTeleport()
    {
        teleport.Play();
    }

    public void PlayTeleport2()
    {
        teleport2.Play();
    }

    public void PlayStartSummon()
    {
        startSummon.Play();
    }

    public void PlayZombieStart()
    {
        zombieStart.Play();
    }

    public void PlayDeath()
    {
        death.Play();
    }
}

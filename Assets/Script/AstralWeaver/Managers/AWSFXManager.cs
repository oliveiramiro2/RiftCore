using UnityEngine;

public class AWSFXManager : MonoBehaviour
{
    public SoundData energyBallSound;
    public SoundData laserSound;
    public SoundData multiLasersSound;
    public SoundData crystalsSound;
    public SoundData shieldSound;
    public SoundData teleportSound;

    public void PlayEnergyBallSound()
    {
        energyBallSound.Play();
    }

    public void PlayLaserSound()
    {
        laserSound.Play();
    }

    public void PlayMultiLasersSound()
    {
        multiLasersSound.Play();
    }

    public void PlayCrystalsSound()
    {
        crystalsSound.Play();
    }

    public void PlayShieldSound()
    {
        shieldSound.Play();
    }

    public void PlayTeleportSound()
    {
        teleportSound.Play();
    }

}

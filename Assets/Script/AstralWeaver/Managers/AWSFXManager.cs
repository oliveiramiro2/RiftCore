using UnityEngine;

public class AWSFXManager : MonoBehaviour
{
    public SoundData energyBallSound;
    public SoundData laserSound;

    public void PlayEnergyBallSound()
    {
        energyBallSound.Play();
    }

    public void PlayLaserSound()
    {
        laserSound.Play();
    }
}

using UnityEngine;

public class AWSFXManager : MonoBehaviour
{
    public SoundData energyBallSound;
    public SoundData laserSound;
    public SoundData multiLasersSound;
    public SoundData crystalsSound;
    public SoundData shieldSound;
    public SoundData teleportSound;
    public SoundData enemyDeathSound;
    public SoundData laughSound;
    public SoundData sighSound;
    public SoundData screamSound;

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

    public void PlayEnemyDeathSound()
    {
        enemyDeathSound.Play();
    }

    public void PlayLaughSighSound()
    {
        int aux = Random.Range(0, 2);

        if (aux == 0)
            laughSound.Play();
        else
            sighSound.Play();
    }

    public void PlayScreamSound()
    {
        screamSound.Play();
    }
}

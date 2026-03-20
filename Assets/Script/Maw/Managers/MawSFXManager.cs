using UnityEngine;

public class MawSFXManager : MonoBehaviour
{
    public SoundData explosion;

    public void PlayExplosion()
    {
        explosion.Play();
    }
}

using UnityEngine;

public class FxTet : MonoBehaviour
{
    public ParticleSystem fxDash;
    public ParticleSystem fxExplosion;
    public ParticleSystem fxSparks;

    public void PlayDashFX()
    {
        fxDash.Play();
    }

    public void PlayExplosionFX()
    {
        fxExplosion.Play();
    }

    public void PlaySparksFX()
    {
        fxSparks.Play();
    }
}

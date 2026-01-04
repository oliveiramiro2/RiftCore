using UnityEngine;

public class FXPlayer : MonoBehaviour
{
    public ParticleSystem sparkDashFX;
    public ParticleSystem landSmockFX;

    public void PlaySparkDashFX()
    {
        sparkDashFX.Play();
    }

    public void PlayLandSmockFX()
    {
        landSmockFX.Play();
    }
}

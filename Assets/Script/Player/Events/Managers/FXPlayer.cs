using Unity.VisualScripting;
using UnityEngine;

public class FXPlayer : MonoBehaviour
{
    public ParticleSystem sparkDashFX;
    public ParticleSystem landSmockFX;
    public ParticleSystem hitEnemyRightFX;
    public ParticleSystem hitEnemyLeftFX;
    public ParticleSystem focusBuffFX;
    public ParticleSystem backgroundFocusBuffFX;
    public ParticleSystem buffFX;

    [SerializeField] private Transform playerTransform;

    public void PlaySparkDashFX()
    {
        sparkDashFX.Play();
    }

    public void PlayLandSmockFX()
    {
        landSmockFX.Play();
    }

    public void PlayHitEnemyFX()
    {
        Vector3 scale = playerTransform.localScale;
        if (scale.x < 0)
        {
            hitEnemyLeftFX.Play();
        }
        else
        {
            hitEnemyRightFX.Play();
        }
    }

    public void PlayFocusBuffFX()
    {
        backgroundFocusBuffFX.Play();
        focusBuffFX.Play();
    }

    public void PlayBuffFX()
    {
        buffFX.Play();
    }
}

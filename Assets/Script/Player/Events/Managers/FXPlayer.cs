using Unity.VisualScripting;
using UnityEngine;

public class FXPlayer : MonoBehaviour
{
    public ParticleSystem sparkDashFX;
    public ParticleSystem landSmockFX;
    public ParticleSystem hitEnemyRightFX;
    public ParticleSystem hitEnemyLeftFX;

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
}

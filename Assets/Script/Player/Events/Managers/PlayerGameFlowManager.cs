using UnityEngine;

public class PlayerGameFlowManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem playerHitSparksLeft;
    [SerializeField] private ParticleSystem playerHitSparksRight;

    public void SwordBuffEffect()
    {
        var mainLeft = playerHitSparksLeft.main;
        mainLeft.startColor = Color.red;
        var mainRight = playerHitSparksRight.main;
        mainRight.startColor = Color.red;
    }

    public void ResetPlayerHitSparksEffect()
    {
        var mainLeft = playerHitSparksLeft.main;
        mainLeft.startColor = Color.white;
        var mainRight = playerHitSparksRight.main;
        mainRight.startColor = Color.white;
    }
}

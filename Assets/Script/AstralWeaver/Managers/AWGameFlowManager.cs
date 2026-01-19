using UnityEngine;

public class AWGameFlowManager : MonoBehaviour
{
  [SerializeField] private ParticleSystem playerHitSparksLeft;
  [SerializeField] private ParticleSystem playerHitSparksRight;


  public void ShieldIsActiveEffect()
  {
    var mainLeft = playerHitSparksLeft.main;
    mainLeft.startColor = Color.cyan;
    var mainRight = playerHitSparksRight.main;
    mainRight.startColor = Color.cyan;
  }

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
using UnityEngine;


[CreateAssetMenu(menuName = "Events/Controllers/AstralWeaverEvents", fileName = "AstralWeaverEvents")]
public class AstralWeaverEvents : ScriptableObject
{
  public GameEvent OnEnergyBall;
  public GameEvent OnLaser;
  public GameEvent OnMultiLasers;
  public GameEvent OnCrystals;
  public GameEvent OnShield;
  public GameEvent OnShieldEnd;
  public GameEvent OnTeleportOut;
  public GameEvent OnTeleportIn;
  public GameEvent OnPhase2;
  public GameEvent OnHurt;
  public GameEvent OnDeath;
}
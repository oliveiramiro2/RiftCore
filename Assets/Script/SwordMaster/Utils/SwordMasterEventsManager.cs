using UnityEngine;

[CreateAssetMenu(menuName = "Events/Controllers/SwordMasterEvents", fileName = "SwordMasterEvents")]
public class SwordMasterEventsManager : ScriptableObject
{
  public GameEvent Slash1;
  public GameEvent Slash2;
  public GameEvent Slash3;
  
  public GameEvent Storm;
  public GameEvent AirSlash;
  public GameEvent Teleport;
  
  public GameEvent Footstep;
  public GameEvent Laughter;
  public GameEvent Explosion;

  
  public GameEvent Parry;
  public GameEvent CounterAttack;
  
  public GameEvent Breathing;
  public GameEvent BreathIn;
}
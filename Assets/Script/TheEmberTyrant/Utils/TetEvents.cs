using UnityEngine;


[CreateAssetMenu(menuName = "Events/Controllers/TetEvents", fileName = "TetEvents")]
public class TetEvents : ScriptableObject
{
  public GameEvent OnFireBall;
  public GameEvent OnFirePillar;
  public GameEvent OnExplosion;
  public GameEvent OnPunch;
  public GameEvent OnDash;
  public GameEvent OnPhase2;
  public GameEvent OnDeath;
}
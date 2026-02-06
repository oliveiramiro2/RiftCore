using UnityEngine;

[CreateAssetMenu(menuName = "Events/Controllers/ToxicSlimeEvents", fileName = "ToxicSlimeEvents")]
public class ToxicSlimeEventsManager : ScriptableObject
{
  public GameEvent OnToxicRainStart;
  public GameEvent OnToxicRainCloundAppear;
  public GameEvent OnToxicDeath;
  public GameEvent OnToxicHurt;
  public GameEvent OnToxicPhase2;
  public GameEvent OnToxicJumpImpact;
  public GameEvent OnToxicProjectilLand;
  public GameEvent OnToxicProjectilExplosion;
  public GameEvent OnToxicRoll;
  public GameEvent OnToxicVomit;
  public GameEvent OnToxicRegular;
  public GameEvent OnToxicRainFall;
  public GameEvent OnToxicSlap;
}
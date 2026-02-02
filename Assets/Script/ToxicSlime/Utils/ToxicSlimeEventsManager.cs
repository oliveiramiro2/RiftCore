using UnityEngine;

[CreateAssetMenu(menuName = "Events/Controllers/ToxicSlimeEvents", fileName = "ToxicSlimeEvents")]
public class ToxicSlimeEventsManager : ScriptableObject
{
  public GameEvent OnToxicRainStart;
  public GameEvent OnToxicRainCloundAppear;
}
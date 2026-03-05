using UnityEngine;

[CreateAssetMenu(menuName = "Events/Controllers/SwordMasterEvents", fileName = "SwordMasterEvents")]
public class SwordMasterEventsManager : ScriptableObject
{
  public GameEvent Slash1;
  public GameEvent Slash2;
  public GameEvent Slash3;
}
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Controllers/LasterEvents", fileName = "LasterEvents")]
public class LasterEventsManager : ScriptableObject
{
    public GameEvent Hurt;
    public GameEvent Phase2;
    public GameEvent Death;
    public GameEvent TeleportIn;
    public GameEvent TeleportOut;
}
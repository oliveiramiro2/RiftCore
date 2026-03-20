using UnityEngine;

[CreateAssetMenu(menuName = "Events/Controllers/MawEvents", fileName = "MawEvents")]
public class MawEventsManager : ScriptableObject
{
    public GameEvent Hurt;
    public GameEvent Phase2;
    public GameEvent Death;


}
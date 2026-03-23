using UnityEngine;

[CreateAssetMenu(menuName = "Events/Controllers/MawEvents", fileName = "MawEvents")]
public class MawEventsManager : ScriptableObject
{
    public GameEvent Hurt;
    public GameEvent Phase2;
    public GameEvent Death;
    public GameEvent Explosion;
    public GameEvent SummonStaff;
    public GameEvent StaffHitFloor;
    public GameEvent Floating;
    public GameEvent Teleport;
    public GameEvent Teleport2;
    public GameEvent StartSummon;
    public GameEvent ZombieStart;
}
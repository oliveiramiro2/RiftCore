using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private Hitbox hitbox;

    public void Activate() => hitbox.Activate();
    public void Deactivate() => hitbox.Deactivate();
}
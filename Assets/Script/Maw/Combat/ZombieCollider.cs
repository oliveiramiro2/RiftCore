using UnityEngine;

public class ZombieCollider : MonoBehaviour
{
    public BoxCollider2D hitbox;

    public void EnableZombieHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableZombieHitbox()
    {
        hitbox.enabled = false;
    }
}

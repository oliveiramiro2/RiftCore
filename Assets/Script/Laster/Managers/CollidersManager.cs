using UnityEngine;

public class CollidersManager : MonoBehaviour
{
    public BoxCollider2D slashCollider;

    public void EnableSlashCollider()
    {
        slashCollider.enabled = true;
    }

    public void DisableSlashCollider()
    {
        slashCollider.enabled = false;
    }
}

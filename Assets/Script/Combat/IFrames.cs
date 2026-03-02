using UnityEngine;

public class IFrames : MonoBehaviour
{

    public void EnableIFrames(Collider2D owner)
    {
        owner.enabled = false;
    }

    public void DisableIFrames(Collider2D owner)
    {
        owner.enabled = true;
    }
}

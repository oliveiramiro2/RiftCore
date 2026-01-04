using UnityEngine;

public class IFrames : MonoBehaviour
{

    public void EnableIFrames(Collider2D owen)
    {
        owen.enabled = false;
    }

    public void DisableIFrames(Collider2D owen)
    {
        owen.enabled = true;
    }
}

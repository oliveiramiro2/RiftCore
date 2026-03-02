using UnityEngine;

public class IFrames : MonoBehaviour
{

    public void EnableIFrames(BoxCollider2D owner)
    {
        owner.enabled = false;
    }

    public void DisableIFrames(BoxCollider2D owner)
    {
        owner.enabled = true;
    }
}

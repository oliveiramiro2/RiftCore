using UnityEngine;

public class SFXSMManager : MonoBehaviour
{
    public SoundData slash1;
    public SoundData slash2;
    public SoundData slash3;

    public void PlaySlash1()
    {
        slash1.Play();
    }

    public void PlaySlash2()
    {
        slash2.Play();
    }

    public void PlaySlash3()
    {
        slash3.Play();
    }
}

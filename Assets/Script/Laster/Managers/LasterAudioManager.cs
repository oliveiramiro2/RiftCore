using UnityEngine;

public class LasterAudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip phase2Music;

    public void PlayPhase2Music()
    {
        musicSource.clip = phase2Music;
        musicSource.loop = true;
        musicSource.Play();
    }
}

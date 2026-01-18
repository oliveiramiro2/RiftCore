using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager Instance;

  [Header("Main Sources")]
  public AudioSource musicSource;
  public AudioSource ambientSource;
  public AudioSource sfxSource;

  [Header("Music / Ambient Clips")]
  public AudioClip musicClip;
  public AudioClip ambientClip;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else Destroy(gameObject);
  }

  private void Start()
  {
    PlayMusic(musicClip);
    PlayAmbient(ambientClip);
  }

  public void PlayMusic(AudioClip clip)
  {
    if (clip == null) return;
    musicSource.clip = clip;
    musicSource.loop = true;
    ambientSource.volume = 0.6f;
    musicSource.Play();
  }

  public void PlayAmbient(AudioClip clip)
  {
    if (clip == null) return;
    ambientSource.clip = clip;
    ambientSource.loop = true;
    ambientSource.volume = 0.1f;
    ambientSource.Play();
  }

  public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
  {
    if (clip == null) return;
    sfxSource.pitch = pitch;
    sfxSource.PlayOneShot(clip, volume);
  }
}
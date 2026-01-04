using UnityEngine;

[CreateAssetMenu(menuName = "Sound/Sound Data", fileName = "SoundData")]
public class SoundData : ScriptableObject
{
    public AudioClip[] clips;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.01f, 2f)] public float pitch = 1f;

    // ✅ método simplificado — aceita AudioSource opcional
    public void Play(AudioSource source = null)
    {
        if (clips == null || clips.Length == 0) return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        if (clip == null) return;

        // se não informaram um AudioSource, usa o AudioManager global
        if (source == null)
            AudioManager.Instance.PlaySFX(clip, volume, Random.Range(pitch, pitch + 0.1f));
        else
        {
            source.pitch = pitch;
            source.PlayOneShot(clip, volume);
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;
    public Image fader;
    public TextMeshProUGUI deathText;
    public AudioSource musicSource;
    public GameObject tetDeathEffect;
    public Image tetFader;
    private BossController bossController;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bossController = FindAnyObjectByType<BossController>();
    }

    public void PlayerDied()
    {
        bossController.canMove = false;
        StartCoroutine(DeathFlow());
    }

    public void TetDied()
    {
        StartCoroutine(DeathFlowTET());
    }

    IEnumerator DeathFlowTET()
    {
        yield return new WaitForSecondsRealtime(7f);

        // InputSystem.actions.FindActionMap("Player").Disable();
        // InputSystem.actions.FindActionMap("UI").Enable();
        tetDeathEffect.SetActive(true);
        StartCoroutine(FadeOutMusic(4f));
        for (float i = 0; i <= 100; i += Time.unscaledDeltaTime * 0.1f)
        {
            tetFader.color = new Color(0, 0, 0, i);
        }
        Time.timeScale = 0f;
    }

    IEnumerator DeathFlow()
    {
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(FadeOutMusic(4f));
        for (float i = 0; i <= 100; i += Time.unscaledDeltaTime * 0.001f)
        {
            fader.color = new Color(0, 0, 0, i);
        }
        deathText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        musicSource.volume = 0f;
    }
}
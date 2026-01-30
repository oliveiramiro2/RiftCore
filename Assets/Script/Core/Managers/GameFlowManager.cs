using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;
    public Image faderDeath;
    public TextMeshProUGUI deathText;
    public AudioSource musicSource;
    public GameObject BossDeathEffect;
    public Image BossFader;
    [SerializeField] private GameObject bossController;

    void Awake()
    {
        Instance = this;
    }

    public void PlayerDied()
    {
        bossController.GetComponent<BaseEntity>().canMove = false;
        StartCoroutine(DeathFlow());
    }

    public void BossDied()
    {
        StartCoroutine(DeathFlowBoss());
    }

    IEnumerator DeathFlowBoss()
    {
        yield return new WaitForSecondsRealtime(7f);

        // InputSystem.actions.FindActionMap("Player").Disable();
        // InputSystem.actions.FindActionMap("UI").Enable();
        BossDeathEffect.SetActive(true);
        StartCoroutine(FadeOutMusic(4f));
        for (float i = 0; i <= 100; i += Time.unscaledDeltaTime * 0.1f)
        {
            BossFader.color = new Color(0, 0, 0, i);
        }
        Time.timeScale = 0f;
    }

    IEnumerator DeathFlow()
    {
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(FadeOutMusic(4f));
        for (float i = 0; i <= 100; i += Time.unscaledDeltaTime * 0.001f)
        {
            faderDeath.color = new Color(0, 0, 0, i);
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
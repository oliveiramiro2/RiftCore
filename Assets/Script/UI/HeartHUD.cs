using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartHUD : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartContainer;

    [Header("Config")]
    [SerializeField] private int healthPerHeart = 4;

    private List<Image> hearts = new List<Image>();

    private void Start()
    {
        PlayerDamageHandler player = FindAnyObjectByType<PlayerDamageHandler>();

        if (player != null)
        {
            UpdateHearts(player.currentHealth, player.maxHealth);
        }
        else
        {
            Debug.LogWarning("HeartHUD: PlayerDamageHandler não encontrado.");
        }
    }

    void OnEnable()
    {
        PlayerDamageHandler.OnHealthChanged += UpdateHearts;
    }

    void OnDisable()
    {
        PlayerDamageHandler.OnHealthChanged -= UpdateHearts;
    }

    private void CreateHearts(int maxHealth)
    {
        // limpa se já existir
        foreach (Transform child in heartContainer)
            Destroy(child.gameObject);

        hearts.Clear();

        int totalHearts = Mathf.CeilToInt((float)maxHealth / healthPerHeart);

        for (int i = 0; i < totalHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            Image img = heart.GetComponent<Image>();
            hearts.Add(img);
        }
    }

    private void UpdateHearts(int currentHealth, int maxHealth)
    {
        if (hearts.Count == 0)
            CreateHearts(maxHealth);

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartMin = i * healthPerHeart;
            int heartMax = heartMin + healthPerHeart;

            float fill = Mathf.Clamp01(
                (currentHealth - heartMin) / (float)healthPerHeart
            );

            hearts[i].fillAmount = fill;
        }
    }
}

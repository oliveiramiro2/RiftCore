using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //public Image healthFill; // a imagem da barra
    private PlayerDamageHandler player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerDamageHandler>();
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (player == null) return;

        float fill = (float)player.currentHealth / player.maxHealth;
        //healthFill.fillAmount = fill;
    }

    public void ShowGameOverScreen()
    {
        Debug.Log("GAME OVER!");
        // ativa UI de game over
    }
}

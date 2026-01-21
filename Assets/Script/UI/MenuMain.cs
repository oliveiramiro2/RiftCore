using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuMain : MonoBehaviour
{
    [SerializeField] private Image chooseEnemyImage;

    void Start()
    {
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindActionMap("UI").Enable();
    }

    public void StartGameTet()
    {
        SceneManager.LoadScene("TETArena");
    }

    public void StartGameAW()
    {
        SceneManager.LoadScene("AWArena");
    }

    public void ChooseEnemy()
    {
        chooseEnemyImage.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

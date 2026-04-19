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

    public void StartGameTS()
    {
        SceneManager.LoadScene("TSArena");
    }

    public void StartGameSM()
    {
        SceneManager.LoadScene("SMArena");
    }

    public void StartGameMaw()
    {
        SceneManager.LoadScene("MAWArena");
    }

    public void StartGameLaster()
    {
        SceneManager.LoadScene("LasterArena");
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

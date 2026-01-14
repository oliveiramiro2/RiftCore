using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuMain : MonoBehaviour
{
    void Start()
    {
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindActionMap("UI").Enable();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TETArena");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

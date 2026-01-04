using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject menu;

    public void OnPauseEvent()
    {
        Time.timeScale = 0f;
        ShowMenu();
    }

    public void OnUnpauseEvent()
    {
        Time.timeScale = 1f;
        HideMenu();
    }

    private void ShowMenu()
    {
        menu.SetActive(true);
    }
    private void HideMenu()
    {
        menu.SetActive(false);
    }
}

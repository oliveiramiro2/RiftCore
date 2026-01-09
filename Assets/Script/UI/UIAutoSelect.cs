using UnityEngine;
using UnityEngine.EventSystems;

public class UIAutoSelect : MonoBehaviour
{
    public GameObject firstSelectedButton;

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject + " in UIAutoSelect");
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }
}
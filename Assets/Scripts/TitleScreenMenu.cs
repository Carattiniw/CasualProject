using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScreenMenu : MonoBehaviour
{
    public string newGameScene;
    public string backToMainMenu;
    public string controlMenu;
    public string creditsScreen;
    public EventSystem ES;
    private GameObject StoreSelected;

    void Start()
    {
        ES = FindObjectOfType<EventSystem>();
        StoreSelected = ES.firstSelectedGameObject;
    }

    void Update()
    {
        if (ES.currentSelectedGameObject != StoreSelected)
        {
            //this code stops a bug where if you accidentally click a mouse button on the pause menu while 
            //using the controller, you would be unable to use the controller to exit the pause menu.
            if (ES.currentSelectedGameObject == null)
            {
                ES.SetSelectedGameObject(StoreSelected);
            }
            else
            {
                StoreSelected = ES.currentSelectedGameObject;
            }
        }
    }

    public void newGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(backToMainMenu);
    }

    public void openControlsMenu()
    {
        SceneManager.LoadScene(controlMenu);
    }

    public void openCredits()
    {
        SceneManager.LoadScene(creditsScreen);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

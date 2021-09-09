using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public string backToMainMenu;
    public GameObject pauseScreen;
    public GameObject controlMenu;
    public GameObject controlMenuButton;
    public GameObject resumeMenuButton;
    public EventSystem ES;
    private GameObject StoreSelected;
    private bool isGamePaused;
    private bool viewedControls;


    void Start()
    {
        StoreSelected = ES.firstSelectedGameObject;
        unpauseGame(); //prevents level from being frozen if player begins game after exiting to main menu
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

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
        {
            if (isGamePaused == true)
            {
                unpauseGame();
            }
            else
            {
                pauseGame();
            }
        }

        if (controlMenu.activeSelf == true)
        {
            showControlsScreen(); //calls method to force the back to pause menu to be selected
        }

        if (controlMenu.activeSelf == false)
        {
            if (isGamePaused == true && viewedControls == true)
            {
                hideControlsScreen();
                //only way I could figure out how to get the resume button to be selected again after viewing controls screen
            }
        }
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(backToMainMenu);
    }

    public void pauseGame()
    {
        isGamePaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; //freezes game
        //GameManager.Instance.pause;
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(resumeMenuButton, null);
    }

    public void unpauseGame()
    {
        isGamePaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f; //unfreeze game
    }

    public void showControlsScreen()
    {
        viewedControls = true;
        controlMenu.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(controlMenuButton, null);
    }

    public void hideControlsScreen()
    {
        viewedControls = false;
        controlMenu.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(resumeMenuButton, null);
    }
}

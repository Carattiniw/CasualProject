using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public string backToMainMenu;
    public string levelNameToRestart;
    public GameObject pauseScreen;
    public GameObject controlMenu;
    public GameObject restartLevelScreen;
    public GameObject controlMenuButton;
    public GameObject resumeMenuButton;
    public GameObject restartLevelButton;
    public EventSystem ES;
    private GameObject StoreSelected;
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
            if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Credits" && SceneManager.GetActiveScene().name != "Controls")
            {

                if (GameManager.Instance.pause == true)
                {
                    unpauseGame();
                }
                else
                {
                    pauseGame();
                }
            }
        }

        if (controlMenu.activeSelf == true)
        {
            showControlsScreen(); //calls method to force the back to pause menu to be selected
        }

        if (controlMenu.activeSelf == false)
        {
            if (GameManager.Instance.pause == true && viewedControls == true)
            {
                hideControlsScreen();
                //only way I could figure out how to get the resume button to be selected again after viewing controls screen
            }
        }
    }

    public void returnToMainMenu()
    {
        //pauseScreen.SetActive(false); test to get rid of dark main menu after play
        SceneManager.LoadScene(backToMainMenu);
    }

    public void pauseGame()
    {
        GameManager.Instance.pause = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; //freezes game
        MusicManager.Instance.audioSource.Pause();
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(resumeMenuButton, null);
    }

    public void unpauseGame()
    {
        GameManager.Instance.pause = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f; //unfreeze game
        MusicManager.Instance.audioSource.UnPause();
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

    public void restartLevel()
    {
        Time.timeScale = 0f;
        restartLevelScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(restartLevelButton, null);
        SceneManager.LoadScene(levelNameToRestart);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool pause = false;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI previousBest;
    public float currentTime;

    public string tutorialBest;
    public string level1Best;
    public string level2Best;
    public string level3Best;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            tutorialBest = "--:--.-";
            level1Best = "--:--.-";
            level2Best = "--:--.-";
            level3Best = "--:--.-";
            timer = GetComponentInChildren<TextMeshProUGUI>();

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Controls" || SceneManager.GetActiveScene().name == "Credits" || SceneManager.GetActiveScene().name == "ArtGallery")
        {
            timer.text = "";
            previousBest.text = "";
        }
        else
        {
            currentTime += Time.deltaTime;
            timer.text = "CT: " + currentTime.ToString("00.0");
            DisplayPreviousBest(SceneManager.GetActiveScene().name);
        }
    }
    public void DisplayPreviousBest(string sceneName)
    {
        switch (sceneName)
        {
            case "Tutorial":
                previousBest.text = "PB: " + tutorialBest;
                return;
            case "Level 1":
                previousBest.text = "PB: " + level1Best;
                return;
            case "Level 2":
                previousBest.text = "PB: " + level2Best;
                return;
            case "Level 3":
                previousBest.text = "PB: " + level3Best;
                return;
            default:
                previousBest.text = "";
                return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {get; private set; }
    public AudioSource audioSource;
    public AudioClip menu;
    public AudioClip level;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
    Destroy(gameObject);
    }

    private void Update()
    {
        //Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Controls" || SceneManager.GetActiveScene().name == "Credits")
        {
            if (audioSource.clip != menu)
            {
                audioSource.clip = menu;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip != level)
            {
                audioSource.clip = level;
                audioSource.Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadNextScene : MonoBehaviour
{
    public string loadNextLevel;
    public GameObject winScreen;
    public GameObject backToMainMenu;
    public EventSystem ES;
    [SerializeField]private GameObject StoreSelected;

    private void Start()
    {
        ES = FindObjectOfType<EventSystem>();
        StoreSelected = ES.firstSelectedGameObject;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "ArtGallery")
            {
                winScreen.SetActive(true);
                StoreSelected = GameObject.Find("Main Menu Button");
                GameManager.Instance.pause = true;
                return;
            }
            SceneManager.LoadScene(loadNextLevel);
        }
    }
}

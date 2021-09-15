using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string loadNextLevel;
    private Rigidbody rb;


    void OnCollisionStay(Collision collision)
    {
        SceneManager.LoadScene(loadNextLevel);
    }
}

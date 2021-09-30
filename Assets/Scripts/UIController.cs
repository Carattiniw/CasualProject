using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Animator animator;
    public AnimatorStateInfo animatorState;
    public Image image;

    public PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerController>();
        /*Pause and Main Menu check*/
        if (SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Controls" || SceneManager.GetActiveScene().name == "Credits")
        {
            image.enabled = false;
        }
        else if (SceneManager.GetActiveScene().name != "Main Menu" || SceneManager.GetActiveScene().name != "Controls" || SceneManager.GetActiveScene().name != "Credits")
        {
            image.enabled = true;
            if (GameManager.Instance.pause == false)
            {
                animator.SetInteger("CurrentState", player.currentStateIndex);

                if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
                {
                    if (player.GetComponent<PlayerController>().isTraversing == false)
                    {
                        animator.SetTrigger("Transitioning");
                    }
                }
            }
        }
    }
}

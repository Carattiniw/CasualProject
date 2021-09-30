using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Animator animator;
    public AnimatorStateInfo animatorState;

    public PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerController>();
        /*Pause and Main Menu check*/
        if (GameManager.Instance.pause == false)
        {
            animator.SetInteger("CurrentState", player.currentStateIndex);
        }
    }
    public void SetTrigger()
    {
        //animator.SetTrigger("Transitioning")
    }    
}

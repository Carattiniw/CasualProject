using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Image imgGas;
    [SerializeField]
    private Image imgSolid;
    [SerializeField]
    private Image imgLiquid;

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
            
        }
    }

    public void EnableStateIcons()
    {
        imgSolid.enabled = true;
        imgLiquid.enabled = true;
        imgGas.enabled = true;
    }

    public void DisableStateIcons()
    {
        imgSolid.enabled = false;
        imgLiquid.enabled = false;
        imgGas.enabled = false;
    }
}

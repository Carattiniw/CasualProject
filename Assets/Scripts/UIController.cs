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
            if (player.currentStateString == "Solid")
            {
                //Debug.Log(currentStateString);
                imgSolid.material = new Material(imgSolid.material);
                imgSolid.material.color = Color.Lerp(Color.white, Color.grey, Mathf.PingPong(Time.time, 1));
                imgSolid.rectTransform.localPosition = new Vector3(51, -20);
                imgSolid.rectTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

                //make inactive states grey on the UI and move them
                imgGas.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgGas.rectTransform.localPosition = new Vector3 (-30, 34, 0);
                imgGas.rectTransform.localScale = new Vector3 (0.5f, 0.5f);

                imgLiquid.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgLiquid.rectTransform.localPosition = new Vector3(51, 69, 0);
                imgLiquid.rectTransform.localScale = new Vector3(0.7f, 0.7f);
            }
            if (player.currentStateString == "Liquid")
            {
                //Debug.Log(currentStateString);
                imgLiquid.material = new Material(imgSolid.material);
                imgLiquid.material.color = Color.Lerp(Color.white, Color.grey, Mathf.PingPong(Time.time, 1));
                imgLiquid.rectTransform.localPosition = new Vector3(55, -30);
                imgLiquid.rectTransform.localScale = new Vector3(1, 1);

                //make inactive states grey on the UI and move them
                imgSolid.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgSolid.rectTransform.localPosition = new Vector3(-30, 38);
                imgSolid.rectTransform.localScale = new Vector3(0.5f, 0.5f);

                imgGas.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgGas.rectTransform.localPosition = new Vector3(52, 70);
                imgGas.rectTransform.localScale = new Vector3(0.5f, 0.5f);
            }
            if (player.currentStateString == "Gas")
            {
                //Debug.Log(currentStateString);
                imgGas.material = new Material(imgSolid.material);
                imgGas.material.color = Color.Lerp(Color.white, Color.grey, Mathf.PingPong(Time.time, 1));
                imgGas.rectTransform.localPosition = new Vector3(52.5f, -28.6f);
                imgGas.rectTransform.localScale = new Vector3(0.85f, 0.85f);

                //make inactive states grey on the UI and move them
                imgSolid.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgSolid.rectTransform.localPosition = new Vector3(50, 75);
                imgSolid.rectTransform.localScale = new Vector3(0.5f, 0.5f);

                imgLiquid.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgLiquid.rectTransform.localPosition = new Vector3(-28, 33);
                imgLiquid.rectTransform.localScale = new Vector3(0.7f, 0.7f);
            }
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

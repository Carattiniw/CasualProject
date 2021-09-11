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

    public CharacterController controller;

    public bool tutorialSolidOnly;
    public bool tutorialGasOnly;
    public bool tutorialLiquidOnly;

    [Header("State of Matter")]
    //States: 0 = Solid, 1 = Liquid, 2 = Gas | ARRAY FIRST INDEX IS 0 NOT 1
    [HideInInspector] public string[] states = { "Solid", "Liquid", "Gas" };
    public int currentStateIndex = 0;
    public string currentStateString;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (tutorialSolidOnly == true)
        {
            currentStateIndex = 0;
        }

        if (tutorialLiquidOnly == true)
        {
            currentStateIndex = 1;
        }

        if (tutorialGasOnly == true)
        {
            currentStateIndex = 2;
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*Pause and Main Menu check*/
        if (GameManager.Instance.pause == false)
        {
            //Track current state;
            currentStateString = states[currentStateIndex];

            /*Cycle forward one state, looping back from solid to gas*/
            if (Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1"))
            {
                if (tutorialSolidOnly == true)
                {
                    return;
                }

                if (tutorialLiquidOnly == true)
                {
                    return;
                }

                if (tutorialGasOnly == true)
                {
                    return;
                }


                if (currentStateIndex >= states.Length - 1)
                {
                    currentStateIndex = 0;
                }
                else
                    currentStateIndex += 1;
            }

            /*Cycle backward one state, looping back to gas from solid*/
            if (Input.GetButtonUp("Fire2"))
            {
                if (tutorialSolidOnly == true)
                {
                    return;
                }

                if (tutorialLiquidOnly == true)
                {
                    return;
                }

                if (tutorialGasOnly == true)
                {
                    return;
                }


                if (currentStateIndex <= 0)
                {
                    currentStateIndex = 2;
                }
                else
                    currentStateIndex -= 1;
            }
        }
        else
            return;
    }



    private void FixedUpdate()
    {
        if (GameManager.Instance.pause == false)
        {
            if (currentStateString == "Solid")
            {
                //Debug.Log(currentStateString);
                imgSolid.material = new Material(imgSolid.material);
                imgSolid.material.color = Color.Lerp(Color.white, Color.grey, Mathf.PingPong(Time.time, 1));

                //make inactive states grey on the UI
                imgGas.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgLiquid.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
            }
            if (currentStateString == "Liquid")
            {
                //Debug.Log(currentStateString);
                imgLiquid.material = new Material(imgSolid.material);
                imgLiquid.material.color = Color.Lerp(Color.white, Color.grey, Mathf.PingPong(Time.time, 1));

                //make inactive states grey on the UI
                imgSolid.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgGas.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
            }
            if (currentStateString == "Gas")
            {
                //Debug.Log(currentStateString);
                imgGas.material = new Material(imgSolid.material);
                imgGas.material.color = Color.Lerp(Color.white, Color.grey, Mathf.PingPong(Time.time, 1));

                //make inactive states grey on the UI
                imgSolid.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
                imgLiquid.material.color = Color.Lerp(Color.grey, Color.grey, Mathf.PingPong(Time.time, 1));
            }
        }
    }
}

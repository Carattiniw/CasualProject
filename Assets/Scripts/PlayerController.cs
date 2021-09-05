using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed;

    [Header("State of Matter")]
    //States: 0 = Solid, 1 = Liquid, 2 = Gas | ARRAY FIRST INDEX IS 0 NOT 1
    [HideInInspector]public string[] states = { "Solid", "Liquid", "Gas" };
    public int currentStateIndex = 0;
    public string currentStateString;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        /*Pause and Main Menu check*/
        if (GameManager.Instance.pause == false)
        {
            //Track current state;
            currentStateString = states[currentStateIndex];

            /*Cycle forward one state, looping back from solid to gas*/
            if (Input.GetButtonUp("Jump"))
            {
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
                SolidMovement();
            }
            if (currentStateString == "Liquid")
            {
                LiquidMovement();
            }
            if (currentStateString == "Gas")
            {
                GasMovement();
            }
        }
    }
    private void SolidMovement()
    {//Solid State Movement
            speed = 400f;
            float x = Input.GetAxis("Horizontal");
        
            Vector3 move = transform.forward * x;
            controller.SimpleMove(move * speed * Time.deltaTime);
    }
    private void LiquidMovement()
    {//Liquid State Movement
        speed = 1250f;
        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.forward * x;
        controller.SimpleMove(move * speed * Time.deltaTime);
    }
        private void GasMovement()
    {//Gas State Movement
        speed = 2.5f;
        float x = Input.GetAxis("Horizontal") * 2;
        float y = 2f;

        Vector3 move = transform.forward * x + transform.up * y;
        controller.Move(move * speed * Time.deltaTime);
    
    }
}

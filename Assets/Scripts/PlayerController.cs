using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public bool tutorialSolidOnly;
    public bool tutorialGasOnly;
    public bool tutorialLiquidOnly;
    //public float jumpForce;

    private Rigidbody rb;
    private float mass;

    Vector3 jumpForce;

    [Header("State of Matter")]
    //States: 0 = Solid, 1 = Liquid, 2 = Gas | ARRAY FIRST INDEX IS 0 NOT 1
    [HideInInspector]public string[] states = { "Solid", "Liquid", "Gas" };
    public int currentStateIndex = 0;
    public string currentStateString;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        rb.mass = 1.0f;
        jumpForce = new Vector3(0.0f, 10.0f, 0.0f);

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

    private void Update()
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

    void OnCollisionStay(Collision collision)
    {
        //only works if player is touching the ground and is in solid state
        if (collision.collider.tag == "Floor" && currentStateString == "Solid")
        //if (currentStateString == "Solid") //testing only
        //if (collision.collider.tag == "Floor") //testing only
        {
            float x = Input.GetAxis("Horizontal");
            //only jump if player is moving either left or right
            if (x > 0.0f || x < 0.0f)
            {
                Debug.Log("I'm gonna jump!");
                //rb.AddForce((Vector3.up * jumpForce), ForceMode.Impulse);
                rb.AddForce(jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void SolidMovement()
    {//Solid State Movement
        //Debug.Log("Solid");
        speed = 400f;
        rb.mass = 1.0f;
        float x = Input.GetAxis("Horizontal");
        
        Vector3 move = transform.forward * x;
        controller.SimpleMove(move * speed * Time.deltaTime);
    }
    private void LiquidMovement()
    {//Liquid State Movement
        //Debug.Log("Liquid");
        speed = 1250f;
        rb.mass = 1.0f;
        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.forward * x;
        controller.SimpleMove(move * speed * Time.deltaTime);
    }
        private void GasMovement()
    {//Gas State Movement
        //Debug.Log("Gas");
        speed = 2.5f;
        rb.mass = 1.0f;
        float x = Input.GetAxis("Horizontal") * 2;
        float y = 2f;

        Vector3 move = transform.forward * x + transform.up * y;
        controller.Move(move * speed * Time.deltaTime);
    }
}

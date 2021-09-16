using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
   // public CharacterController controller;
    public float speed;
    public float verticalInput;
   
    public bool tutorialSolidOnly;
    public bool tutorialGasOnly;
    public bool tutorialLiquidOnly;

    private Rigidbody rb;

    public Vector3 jumpForce;
    public bool isGrounded;
    public bool isMagnetized;

    [Header("State of Matter")]
    //States: 0 = Solid, 1 = Liquid, 2 = Gas | ARRAY FIRST INDEX IS 0 NOT 1
    [HideInInspector]public string[] states = { "Solid", "Liquid", "Gas" };
    public int currentStateIndex = 0;
    public string currentStateString;

    public Animator animator;

    private void Awake()
    {
        animator = FindObjectOfType<Animator>();
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

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

            /*Cycle forward one state, looping back to solid from gas*/
            if (Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1"))
            {
                if (tutorialSolidOnly == true || tutorialLiquidOnly == true || tutorialGasOnly == true)
                {
                    return;
                }
                else
                {
                    if (currentStateIndex == 2)
                    {
                        animator.SetTrigger("stateChange");
                        currentStateIndex = 0;
                    }
                    else
                    {
                        animator.SetTrigger("stateChange");
                        currentStateIndex += 1;
                    }
                }
            }

            /*Cycle backward one state, looping back to gas from solid*/
            if (Input.GetButtonUp("Fire2"))
            {
                if (tutorialSolidOnly == true || tutorialLiquidOnly == true || tutorialGasOnly == true)
                {
                    return;
                }
                else
                {
                    if (currentStateIndex == 0)
                    {
                        animator.SetTrigger("stateChange");
                        currentStateIndex = 2;
                    }
                    else
                    {
                        animator.SetTrigger("stateChange");
                        currentStateIndex -= 1;
                    }
                }
            }
        }
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
    void OnCollisionEnter(Collision collision)
    {
        //only works if player is touching the ground and is in solid state
        if (collision.collider.tag == "Floor")
        //if (currentStateString == "Solid") //testing only
        //if (collision.collider.tag == "Floor") //testing only
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isGrounded = false;
        }
    }
    private void SolidMovement()
    {//Solid State Movement Parameters

        rb.useGravity = true;
        speed = 45f;
        rb.mass = 0f;

        if (isGrounded == false && isMagnetized == false)
        {
            rb.velocity = new Vector3(0, -25, 0);
        }
        else
        {
            rb.drag = 5f;

            float x = Input.GetAxis("Horizontal");

            Vector3 move = transform.forward * x * speed;
            rb.AddForce(move, ForceMode.Acceleration);
        }
    }
    private void LiquidMovement()
    {//Liquid State Movement Parameters

        if(isGrounded == false)
        {
            rb.AddForce(new Vector3 (0, -9.8f * - 9.8f / -3, 0));
        }

        speed = 125f;
        rb.mass = 1f;
        rb.drag = 2.5f;
        rb.angularDrag = 0.25f;
        rb.useGravity = true;

        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.forward * x * speed;
        rb.AddForce(move, ForceMode.Acceleration);
    }
        private void GasMovement()
    {//Gas State Movement
        //Debug.Log("Gas");
        rb.mass = 5f;
        rb.drag = 5f;
        rb.angularDrag = 0f;
        rb.useGravity = false;
        
        speed = 2.5f;
        rb.mass = 1.0f;
        float x = Input.GetAxis("Horizontal") * 25f;
        float y = 5f;

        Vector3 move = transform.forward * x  * speed + transform.up * y * speed;
        rb.AddForce(move, ForceMode.Acceleration);
    }
}

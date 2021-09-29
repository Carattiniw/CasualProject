using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

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

    private AudioSource audioSource;
    public AudioClip solidAudio;
    public AudioClip liquidAudio;
    private bool playedliquid = false;
    private bool playedSolid = false;

    [Header("State of Matter")]
    //States: 0 = Solid, 1 = Liquid, 2 = Gas | ARRAY FIRST INDEX IS 0 NOT 1
    [HideInInspector]public string[] states = { "Solid", "Liquid", "Gas" };
    public int currentStateIndex = 0;
    public string currentStateString;

    public Animator animator;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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

            if (currentStateString == "Liquid" || currentStateString == "Gas")
            {
                gameObject.layer = 7;
            }
            else
                gameObject.layer = 6;

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
        rb.mass = 1f;

        if (isGrounded == false && isMagnetized == false)
        {
            rb.drag = 0f;
            rb.angularDrag = 0f;
            rb.AddForce(0, -1f, 0, ForceMode.Impulse);
        }
        else
        {
            rb.drag = 5f;

            float x = Input.GetAxis("Horizontal");

            Vector3 move = transform.forward * x * speed;
            rb.AddForce(move, ForceMode.Acceleration);

            if (playedSolid == false && x > 0)
            {
                playedSolid = true;
                StartCoroutine(PlaySolidSound());
            }

            if (playedSolid == false && x < 0)
            {
                playedSolid = true;
                StartCoroutine(PlaySolidSound());
            }
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
        
        if (playedliquid == false && x > 0)
        {
            playedliquid = true;
            StartCoroutine(PlayLiquidSound());
        }

        if (playedliquid == false && x < 0)
        {
            playedliquid = true;
            StartCoroutine(PlayLiquidSound());
        }
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

    IEnumerator PlayLiquidSound()
    {
        audioSource.PlayOneShot(liquidAudio, 1.0F);
        yield return new WaitForSeconds(1);
        playedliquid = false;
    }

    IEnumerator PlaySolidSound()
    {
        audioSource.PlayOneShot(solidAudio, 1.0F);
        yield return new WaitForSeconds(1);
        playedSolid = false;
    }
}

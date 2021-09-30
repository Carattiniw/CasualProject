using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public ShiftyAnimation animate;
    public float speed;
    public float verticalInput;

    private Rigidbody rb;

    public Vector3 jumpForce;
    public bool isMagnetized;
    public bool isTraversing;
    
    public bool isGrounded;
    public SphereCollider groundCheck;

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

    private void Awake()
    {
        animate = GetComponentInChildren<ShiftyAnimation>();
        groundCheck = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*Pause and Main Menu check*/
        if (GameManager.Instance.pause == false)
        {
            //Track current state;
            currentStateString = states[currentStateIndex];
            if (isTraversing == false)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    gameObject.transform.localScale = new Vector3(2.5f, 2.5f, -2.5f);
                }

                if (currentStateString == "Solid")
                {
                    gameObject.layer = 3;
                    if (isGrounded == true && Input.GetAxis("Horizontal") == 0)
                    {
                        if (rb.velocity.z > -0.1f && rb.velocity.z < 0.1f)
                        {
                            animate.ChangeAnimationState(ShiftyAnimation.solidIdle);
                        }
                        else if (rb.velocity.z <= -0.1f || rb.velocity.z >= 0.1f)
                        {
                            animate.ChangeAnimationState(ShiftyAnimation.solidRunning);
                        }
                    }
                    else if (isGrounded == true && Input.GetAxis("Horizontal") != 0)
                    {
                        animate.ChangeAnimationState(ShiftyAnimation.solidRunning);
                    }
                    else if (isGrounded == false && rb.velocity.y < 0f)
                    {
                        animate.ChangeAnimationState(ShiftyAnimation.solidBreaking);
                    }
                }
                else if (currentStateString == "Liquid")
                {
                    gameObject.layer = 6;
                    if (isGrounded == true && Input.GetAxis("Horizontal") == 0)
                    {
                        if (rb.velocity.z > -0.1f && rb.velocity.z < 0.1f)
                        {
                            animate.ChangeAnimationState(ShiftyAnimation.liquidIdle);
                        }
                        else if (rb.velocity.z <= -0.1f || rb.velocity.z >= 0.1f)
                        {
                            animate.ChangeAnimationState(ShiftyAnimation.liquidRunning);
                        }
                    }
                    else if (isGrounded == true && Input.GetAxis("Horizontal") != 0)
                    {
                        if (rb.velocity.z > -0.1f && rb.velocity.z < 0.1f)
                        {
                            animate.ChangeAnimationState(ShiftyAnimation.liquidIdleToLiquidRunning);
                        }
                        else
                        {
                            animate.ChangeAnimationState(ShiftyAnimation.liquidRunning);
                        }
                    }
                    else if (isGrounded == false && rb.velocity.y < 0f)
                    {
                        animate.ChangeAnimationState(ShiftyAnimation.liquidFalling);
                    }
                }
                else if (currentStateString == "Gas")
                {
                    gameObject.layer = 7;
                    if (Input.GetAxis("Horizontal") == 0)
                    {
                        if (rb.velocity.z > -0.1f && rb.velocity.z < 0.1f)
                        {
                            animate.ChangeAnimationState(ShiftyAnimation.gasIdle);
                        }
                        else if (rb.velocity.z <= -0.1f || rb.velocity.z >= 0.1f)
                        {
                            animate.ChangeAnimationState(ShiftyAnimation.gasPushPull);
                        }
                    }
                    else if (Input.GetAxis("Horizontal") != 0)
                    {
                        animate.ChangeAnimationState(ShiftyAnimation.gasPushPull);
                    }
                }
            }
                /*Cycle forward one state, looping back to solid from gas*/
                if (Input.GetButtonUp("Fire1") && isTraversing == false)
                {
                    if (currentStateIndex == 2)
                    {
                        currentStateIndex = 0;
                    }
                    else
                    {
                        currentStateIndex += 1;
                    }
                }

                /*Cycle backward one state, looping back to gas from solid*/
                if (Input.GetButtonUp("Fire2") && isTraversing == false)
                {
                    if (currentStateIndex == 0)
                    {
                        currentStateIndex = 2;
                    }
                    else
                    {
                        currentStateIndex -= 1;
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
                //StartCoroutine(PlaySolidSound());
            }

            if (playedSolid == false && x < 0)
            {
                playedSolid = true;
                //StartCoroutine(PlaySolidSound());
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
            //StartCoroutine(PlayLiquidSound());
        }

        if (playedliquid == false && x < 0)
        {
            playedliquid = true;
            //StartCoroutine(PlayLiquidSound());
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

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }    
    }
    IEnumerator PlayLiquidSound()
    {
        audioSource.PlayOneShot(liquidAudio, 1.0F);
        yield return new WaitForSeconds(1);
        playedliquid = false;
        yield break;
    }

    IEnumerator PlaySolidSound()
    {
        audioSource.PlayOneShot(solidAudio, 1.0F);
        yield return new WaitForSeconds(1);
        playedSolid = false;
        yield break;
    }
}

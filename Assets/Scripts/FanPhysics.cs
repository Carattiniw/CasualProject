using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPhysics : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public string playerCurrentState;

    public GameObject fan;
    public float fanForce;
    public Collider fanZone;
    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponentInChildren<Rigidbody>();
        fan= gameObject;
       fanZone = gameObject.GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        playerCurrentState = player.GetComponentInChildren<PlayerController>().currentStateString;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && (playerCurrentState == "Liquid" || playerCurrentState == "Gas"))
        {
            rb.AddForce(new Vector3(0f, 0f, fanForce), ForceMode.Impulse);
        }
    }
}
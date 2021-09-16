using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPhysics : MonoBehaviour
{
    public GameObject player;
    public PlayerController controller;
    public Rigidbody rb;
    public string playerCurrentState;

    public GameObject magnet;
    public Vector3 magnetism;
    public float magnetForce;
    public Collider magnetZone;
    void Start()
    {
        player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
        rb = player.GetComponent<Rigidbody>();
        magnet = gameObject;
        magnetZone = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        playerCurrentState = player.GetComponentInChildren<PlayerController>().currentStateString;
        magnetism = (magnet.transform.position - player.transform.position) * magnetForce;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && playerCurrentState == "Solid")
        {
            controller.isMagnetized = true;
            rb.AddForce(magnetism, ForceMode.VelocityChange);
        }
        else
            controller.isMagnetized = false;
    }
}

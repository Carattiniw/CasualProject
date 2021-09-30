using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPhysics : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public string playerCurrentState;

    public float fanForce;
    public float maxDistance;
    public float distForce;
    public Collider fanZone;
    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        fanZone = gameObject.GetComponent<CapsuleCollider>();
        maxDistance = this.GetComponent<CapsuleCollider>().height;
    }

    private void Update()
    {
        playerCurrentState = player.GetComponent<PlayerController>().currentStateString;
        distForce = maxDistance - Vector3.Distance(transform.position, player.transform.position);
    }
    private void OnTriggerStay(Collider other)
    {
        if (distForce > 0)
        {
            if (other.gameObject == player && (playerCurrentState == "Liquid" || playerCurrentState == "Gas"))
            {
                rb.AddForce(transform.up * fanForce * distForce, ForceMode.Force);
            }
        }
    }
}
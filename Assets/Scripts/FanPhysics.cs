using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPhysics : MonoBehaviour
{
    public GameObject player;
    public CharacterController charctrlr;
    public string playerCurrentState;

    public GameObject fan;
    public float fanForce;
    public Collider fanZone;
    void Start()
    {
        player = GameObject.Find("Player");
        charctrlr = player.GetComponentInChildren<CharacterController>();
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
            charctrlr.SimpleMove(new Vector3(0f, ((player.transform.position.y - fan.transform.position.y) *fanForce * Time.deltaTime), ((player.transform.position.z - fan.transform.position.z) * fanForce * Time.deltaTime)));
        }
    }
}
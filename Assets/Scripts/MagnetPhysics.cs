using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPhysics : MonoBehaviour
{
    public GameObject player;
    public CharacterController charctrlr;
    public string playerCurrentState;

    public GameObject magnet;
    public float magnetForce;
    public Collider magnetZone;
    void Start()
    {
        player = GameObject.Find("Player");
        charctrlr = player.GetComponentInChildren<CharacterController>();
        magnet = gameObject;
        magnetZone = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        playerCurrentState = player.GetComponentInChildren<PlayerController>().currentStateString;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player && playerCurrentState == "Solid")
        {
            charctrlr.SimpleMove((magnet.transform.position - player.transform.position) * magnetForce * Time.smoothDeltaTime);
        }
    }
}

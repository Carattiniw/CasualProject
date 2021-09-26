using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
    }

    // Update is called once per frame
    private void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Player" && player.currentStateString == "Solid")
        {
            Debug.Log("You're Standing on the Trap door");
            //Play animation of door opening which should also move the box collider with it and allow you through the door
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversableObjects : MonoBehaviour
{
    public GameObject player;
    public string playerState;

    public Transform target;
    public Transform pointA;
    public Transform pointB;
    

    public bool isFreezing;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerState = player.GetComponent<PlayerController>().currentStateString;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && playerState == "Liquid" && isFreezing == false)
        {
            player.transform.position = pointB.position;
        }
        else if(other.gameObject.tag == "Player" && playerState == "Liquid" && isFreezing == true)
        {
            Destroy(player);
            //Die State Here
        }
        if(other.gameObject.tag == "Player" && playerState == "Gas")
        {
            player.transform.position = pointA.position;
        }
    }
}

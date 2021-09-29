using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Trapdoor : MonoBehaviour
{
    PlayerController player;
    private AudioSource audioSource;
    public AudioClip openTrapDoorSound;
    private bool audioPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionStay(Collision collisionInfo)
    {
        //if(collisionInfo.gameObject.tag == "Player" && player.currentStateString == "Solid")
        if(collisionInfo.gameObject.tag == "Player" && player.currentStateString == "Solid" && audioPlayed == false)
        {
            Debug.Log("You're Standing on the Trap door");
            /*
            if (audioPlayed == true)
            {
                return;
            }
            */
            audioSource.PlayOneShot(openTrapDoorSound, 1.0F);
            audioPlayed = true;
            //Play animation of door opening which should also move the box collider with it and allow you through the door
        }
    }
}

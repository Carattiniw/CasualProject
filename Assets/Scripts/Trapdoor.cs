using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Animations;

public class Trapdoor : MonoBehaviour
{
    PlayerController player;
    private AudioSource audioSource;
    public AudioClip openTrapDoorSound;
    public Animator animator;
    private bool audioPlayed = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Player" && player.currentStateString == "Solid")
        {
            Debug.Log("You're Standing on the Trap door");
            if (audioPlayed == false)
            {
            audioSource.PlayOneShot(openTrapDoorSound, 1.0F);
            audioPlayed = true;
            }
            animator.SetInteger("DoorState", 1);      
        }
    }
    public void TrapdoorOpeningToOpen()
    {
        animator.SetInteger("DoorState", 2);
    }
}

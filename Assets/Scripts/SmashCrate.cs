using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashCrate : MonoBehaviour
{
    PlayerController player;
    private AudioSource audioSource;
    public AudioClip smashCrateSound;
    private bool audioPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player" && player.currentStateString == "Solid")
        {
            //Debug.Log("SMASH!");
            if (audioPlayed == false)
            {
                audioSource.PlayOneShot(smashCrateSound, 1.0F);
                audioPlayed = true;
            }
            Destroy(gameObject, 0.3f);
        }
    }
}

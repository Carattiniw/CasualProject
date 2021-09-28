using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenuMusic : MonoBehaviour
{
    private bool playMenuMusic;


    void Start()
    {
        
        playMenuMusic = true;

        if (playMenuMusic == false)
        {
            return;
        }
        
        MenuMusic.Instance.gameObject.GetComponent<AudioSource>().Stop();
    }

    public void resumeMenuMusic()
    {
        playMenuMusic = false;//make menu music play - Will
        MenuMusic.Instance.gameObject.GetComponent<AudioSource>().Play();
    }
}

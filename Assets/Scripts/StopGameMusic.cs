using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGameMusic : MonoBehaviour
{
    private bool playGameMusic;

    void Start()
    {

        playGameMusic = true;

        
        if (playGameMusic == false)
        {
            return;
        }
        

        GameMusic.Instance.gameObject.GetComponent<AudioSource>().Stop();
    }

    public void resumeGameMusic()
    {
        playGameMusic = false;//make menu music play - Will
        GameMusic.Instance.gameObject.GetComponent<AudioSource>().Play();
    }
}

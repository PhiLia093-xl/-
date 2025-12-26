using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChange : MonoBehaviour
{
    public AudioClip[] audios;
    public int BGMNumber;
    private int BGMClickCount;

    private void Awake()
    {
        BGMClickCount = 0;
    }

    public void ChangeTimeLinePlay()
    {
        this.GetComponent<AudioSource>().clip = audios[0];
        this.GetComponent<AudioSource>().Play();
        BGMClickCount = 0;
    }

    public void RNumber()
    {
        BGMClickCount++;
        if(BGMClickCount >= BGMNumber)
        {
            BGMClickCount = 0;
        }
    }
    public void ChangeAudio()
    {
        this.GetComponent<AudioSource>().clip = audios[BGMClickCount];
        this.GetComponent <AudioSource>().Play();
    }
}

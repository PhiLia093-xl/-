using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bgm = Resources.Load<AudioClip>("Audio / Bgm / bgm");
        audioSource.clip = bgm;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

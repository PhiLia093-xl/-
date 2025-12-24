using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGMVolume : MonoBehaviour  //ÒôÁ¿µ÷½Ú
{
    public AudioMixer audioMixer;

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("BGMVolume", value);
    }
}

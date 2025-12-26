using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGMVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle BGMToggle;
    private float Switch;

    public void SetVolume(float volume)
    {
        Switch = volume;
        audioMixer.SetFloat("BGMVolume", volume);
    }

    public void BGMController(float volume) 
    {
    
        if (BGMToggle.isOn)
        {
            audioMixer.SetFloat("BGMController", Switch);
        }
        else
        {
            audioMixer.SetFloat("BGMController", -80);
        }
    
    }
}

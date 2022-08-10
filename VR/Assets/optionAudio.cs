using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audio;
    private float limitVolume = 1f;
    public float currentAudioVolume;
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        currentAudioVolume = 0.5f;
    }

    
    void Update()
    {
        if (currentAudioVolume > limitVolume)
        {
            currentAudioVolume = limitVolume;
        }
        else if (currentAudioVolume <= 0.1f)
        {
            currentAudioVolume = 0.1f;
        }
        
        audio.volume = currentAudioVolume;
    }
}

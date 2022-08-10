using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControlUI : MonoBehaviour
{
    private float curAudioVolume = 0.0f;
    private Transform trans;
    public optionAudio optionAudio;
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curAudioVolume = trans.rotation.x;
        optionAudio.currentAudioVolume += curAudioVolume;
    }
}

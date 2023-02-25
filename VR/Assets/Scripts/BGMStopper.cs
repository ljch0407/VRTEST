using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStopper : MonoBehaviour
{
    public SoundManager _soundManager;

    // Start is called before the first frame update
    void Start()
    {
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>().instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _soundManager.StopBGM();
        }
    }

}

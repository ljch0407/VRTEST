using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WOF_small : MonoBehaviour
{
    public PlayableDirector TimeDirector;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosive")
        {
            TimeDirector.Play();
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster")
        {
            other.gameObject.GetComponent<Monster>().isBlind = true;
        }
    }
}

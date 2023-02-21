using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheckbox : MonoBehaviour
{
    public Transform resultPos;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = resultPos.position;
        }
    }
}

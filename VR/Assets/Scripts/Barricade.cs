using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidBody;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hammer")
        {
            rigidBody.isKinematic = false;
        }
    }
}

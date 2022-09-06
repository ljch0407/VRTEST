using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stool : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isActive;

    public Safe safeInfo;
    void Start()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isActive)
            {
                isActive = true;
                transform.position -= new Vector3(0, 0.1f, 0);
                safeInfo.countOfStoolActive++;
            }
            else if(isActive)
            {
                isActive = false;
                transform.position += new Vector3(0, 0.1f, 0);
                safeInfo.countOfStoolActive--;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stool : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isActive;

    public Safe safeInfo;
    public ParticleSystem StoolEffect;
    void Start()
    {
        isActive = false;
        StoolEffect.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isActive)
            {
                isActive = true;
                StoolEffect.Play();
                transform.position -= new Vector3(0, 0.1f, 0);
                safeInfo.countOfStoolActive++;
            }
            else if(isActive)
            {
                isActive = false;
                StoolEffect.Stop();
                transform.position += new Vector3(0, 0.1f, 0);
                safeInfo.countOfStoolActive--;
            }
        }
    }
}

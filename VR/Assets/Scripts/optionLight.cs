using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionLight : MonoBehaviour
{
    // Start is called before the first frame update
    private float limitIntensity = 10.0f;
    public float currentLightIntensity;
    
    void Start()
    {
        currentLightIntensity = 3;
    }

    
    void Update()
    {
        if (currentLightIntensity > limitIntensity)
        {
            currentLightIntensity = limitIntensity;
        }else if (currentLightIntensity <= 0.2f)
        {
            currentLightIntensity = 0.2f;
        }

    }
}

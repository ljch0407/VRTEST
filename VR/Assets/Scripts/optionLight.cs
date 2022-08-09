using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionLight : MonoBehaviour
{
    // Start is called before the first frame update

    private Light lightSetting;
    public float currentLightIntensity;
    private float limitIntensity = 15.0f;
    void Start()
    {
        lightSetting = GetComponent<Light>();
    }

    
    void Update()
    {
        lightSetting.intensity = currentLightIntensity;
        if (lightSetting.intensity > limitIntensity)
        {
            lightSetting.intensity = limitIntensity;
            currentLightIntensity = limitIntensity;
        }
        else if (lightSetting.intensity <= 0.0f)
        {
            lightSetting.intensity = 0.0f;
            currentLightIntensity = 0.0f;
        }
    }
}

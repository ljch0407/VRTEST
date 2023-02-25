using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLightOption : MonoBehaviour
{
    // Start is called before the first frame update

    public Light lightSetting;
    public optionLight option;
    void Start()
    {
        lightSetting = GetComponent<Light>();
    }
    
    void LateUpdate()
    {
        lightSetting.intensity = option.currentLightIntensity;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControlUI : MonoBehaviour
{
    // Start is called before the first frame update

    private float curLightIntensity = 0;
    private Transform trans;
    public optionLight optionLight;
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(trans.rotation.x) > 0.001)
        {
            curLightIntensity = trans.rotation.x;
            optionLight.currentLightIntensity += curLightIntensity;
        }
    }
}

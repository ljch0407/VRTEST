using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UVLightSource : MonoBehaviour
{
    public List<Material> reveals;
    public Light light;

    public Lantern _lantern;
    // Update is called once per frame


    void Update()
    {
        foreach (var reveal in reveals)
        {
            if (!_lantern.isopen)
            {
                reveal.SetVector("_LightPosition", light.transform.position);
                reveal.SetVector("_LightDirection", -light.transform.forward);
                reveal.SetFloat("_LightAngle", light.spotAngle);
            }
            else{
                reveal.SetVector("_LightPosition", light.transform.position);
                reveal.SetVector("_LightDirection", -light.transform.forward);
                reveal.SetFloat("_LightAngle", 0);
            }
        }
        
    }
}

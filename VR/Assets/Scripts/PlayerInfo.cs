using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerInfo : MonoBehaviour
{

    public int HealthPoint;
    private int HasMagicGemCount;

    private bool IsHasteReady = true;
    private bool IsFlashReady = true;
    private bool IsPathFindReady = true;

    private float HasteCooldown = 5f;
    private float FlashCooldown = 5f;
    private float PathFindCooldown = 10f;

    public Transform menuTransform;
    public Transform PlayerLocationBefore;

    
    void Start()
    {
       
        
        menuTransform.transform.position = new Vector3(6f, 0f, 2f);
        menuTransform.transform.rotation = new Quaternion(0, -0.87f, 0, 0.47f);
        
        HealthPoint = 2;
        HasMagicGemCount = 0;
    }

    void Update()
    {
        if (HealthPoint < 2)
        {
            GameObject postProcess = GameObject.Find("Post-process P");
            Volume postVolume = postProcess.gameObject.GetComponent<Volume>();
            postVolume.enabled = true;
        }
        else
        {
            GameObject postProcess = GameObject.Find("Post-process P");
            Volume postVolume = postProcess.gameObject.GetComponent<Volume>();
            postVolume.enabled = false;
        }
        
    }
}

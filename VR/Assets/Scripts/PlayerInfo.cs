using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
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
    


    void Start()
    {
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

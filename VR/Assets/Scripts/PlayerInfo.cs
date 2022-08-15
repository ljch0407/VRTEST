using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


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

    private InputDeviceCharacteristics rightControllerCharacteristics;
    private InputDeviceCharacteristics leftControllerCharacteristics;
    private InputDevice targetDevice;
    private bool menuButton;
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        
        menuTransform.transform.position = new Vector3(6f, 0f, 2f);
        menuTransform.transform.rotation = new Quaternion(0, -0.87f, 0, 0.47f);
        
        HealthPoint = 2;
        HasMagicGemCount = 0;
        
        rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        
       leftControllerCharacteristics =
            InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

        if (devices.Count > 0)
            targetDevice = devices[0];
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

        if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButton) && menuButton)
        {
            Debug.Log("Menu Button Pressed");
        }
    }



}

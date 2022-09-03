using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerInfo : MonoBehaviour
{

    public int healthPoint;
    private int hasMagicGemCount;

    private bool isHasteReady = true;
    private bool isFlashReady = true;
    private bool isPathFindReady = true;

    private float hasteCooldown = 5f;
    private float flashCooldown = 5f;
    private float pathFindCooldown = 10f;
    
    
    public Transform menuTransform;
    public Transform playerLocationBefore;


    public bool isPaused = false;
    
    private InputDeviceCharacteristics rightControllerCharacteristics;
    private InputDeviceCharacteristics leftControllerCharacteristics;
    private InputDevice targetDevice;
    private bool menuButton;
    public SoundManager _soundManager;
    
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>().instance;
        _soundManager.PlayBGM("BGM0");
 
        
        healthPoint = 2;
        hasMagicGemCount = 0;
        
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
        if (healthPoint < 2)
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
            StartCoroutine(MenuRoomEnter());
            Debug.Log("Menu Room Entered");
        }
    }

    private IEnumerator MenuRoomEnter()
    {
        playerLocationBefore.position = transform.position;
        playerLocationBefore.rotation = transform.rotation;

        transform.position = menuTransform.position;
        transform.rotation = menuTransform.rotation;
        _soundManager.StopBGM();
        _soundManager.PlayBGM("BGM1");
        isPaused = true;

        yield return new WaitForSeconds(2.0f);
    }



}

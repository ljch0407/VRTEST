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

    public AudioSource menuAudioSource;
    public AudioSource gameAudioSource;

    public bool isPaused = false;
    
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
        
        menuAudioSource.Play();
        gameAudioSource.Stop();
        
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

        isPaused = true;
        menuAudioSource.Play();
        gameAudioSource.Stop();
        
        yield return new WaitForSeconds(2.0f);
    }



}

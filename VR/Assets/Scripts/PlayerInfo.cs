using System;
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
    public int hasManaStoneCount;
    
    private float hasteCooldown = 5f;
    private float flashCooldown = 5f;
    private float pathFindCooldown = 10f;
    
    
    public Transform menuTransform;
    public Transform playerLocationBefore;


    public bool isPaused = false;
    public bool possibleToHide = false;
    
    private bool possibleToHaste = true;
    private bool possibleToFlash = true;
    private bool possibleToPathFind = true;

    private InputDeviceCharacteristics rightControllerCharacteristics;
    private InputDeviceCharacteristics leftControllerCharacteristics;
    private InputDevice targetDevice;
    private InputDevice targetDeviceR;
    private bool menuButton;
    private bool hide;
    private bool haste;
    private bool flash;
    private Vector2 move;
    
    
    public SoundManager _soundManager;

    public bool underStatue = false;
    public bool midStatue = false;
    public bool upperStatue = false;

    private HideSpot hideSpotObject = null;

    public ContinuousMoveProviderBase continuousMoveProviderBase;

    public ParticleSystem hasteEffect;
    public ParticleSystem flashEffect;
    public GameObject Flashlight;

    public ParticleSystem hasteCooldownEffect;
    public ParticleSystem flashCooldownEffect;
    
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        hasteEffect.Stop();
        flashEffect.Stop();
        hasteCooldownEffect.Stop();
        flashCooldownEffect.Stop();
  
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>().instance;
        _soundManager.PlayBGM("BGM0");
 
        
        healthPoint = 2;
        hasManaStoneCount = 3;
        
       leftControllerCharacteristics =
            InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
        
        rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDeviceR = devices[0];
        }
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

        if (hasManaStoneCount == 0)
        {
            possibleToHaste = false;
            possibleToFlash = false;
            possibleToPathFind = false;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButton) && menuButton)
        {
            //Menu button mapping
            Debug.Log("Menu Button Pressed");
            StartCoroutine(MenuRoomEnter());
            Debug.Log("Menu Room Entered");
        }
        
        if (targetDeviceR.TryGetFeatureValue(CommonUsages.primaryButton, out hide) && hide)
        {
            //Hide spot use
            Debug.Log("Right primary Pressed");
            if (possibleToHide)
            {
                hideSpotObject.Hide();
            }
        }
        
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out haste) && haste)
        {
            //Haste
            Debug.Log("Left primary Pressed");
            StartCoroutine(Haste());
        }
        
        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out flash) && flash)
        {
            //flash
            Debug.Log("Left secondary Pressed");
            StartCoroutine(Flash());
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis,out move))
        {
            if (move.x != 0 || move.y != 0)
            {
                Debug.Log("Player is moving around");
                StartCoroutine(FootStepStart());
            }
            else
                StartCoroutine(FootStepStop());

        }
        
        if (targetDeviceR.TryGetFeatureValue(CommonUsages.primary2DAxis,out move))
        {
            if (move.x != 0 || move.y != 0)
            {
                Debug.Log("Player is turning around");
                StartCoroutine(FootStepStart());
            }
            else
                StartCoroutine(FootStepStop());
        }
    }

    private IEnumerator MenuRoomEnter()
    {
        playerLocationBefore.position = transform.position;
        playerLocationBefore.rotation = transform.rotation;

        transform.position = menuTransform.position;
        transform.rotation = menuTransform.rotation;
        _soundManager.StopBGM();
        _soundManager.PlayBGM("BGM0");
        isPaused = true;

        yield return new WaitForSeconds(2.0f);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "HideSpot")
        {
            Debug.Log("HideSpotNear");
            possibleToHide = true;
            hideSpotObject = other.gameObject.GetComponent<HideSpot>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "HideSpot")
        {
            Debug.Log("HideSpotAreaExit");
            possibleToHide = false;
            hideSpotObject = null;
        }
    }

    IEnumerator Haste()
    {
        if (possibleToHaste)
        {
            possibleToHaste = false;
            Debug.Log("Haste On");
            hasteEffect.Play();
            continuousMoveProviderBase.moveSpeed = 4;
            hasManaStoneCount--;
        }
        
        yield return new WaitForSeconds(15f);
        continuousMoveProviderBase.moveSpeed = 2;
        hasteEffect.Stop();
        hasteCooldownEffect.Play();
        
        yield return new WaitForSeconds(hasteCooldown);
        possibleToHaste = true;
        hasteCooldownEffect.Stop();
    }
    
    IEnumerator Flash()
    {
        if (possibleToFlash)
        {
            possibleToFlash = false;
            Debug.Log("Flash On");
            Flashlight.gameObject.SetActive(true);
            flashEffect.Play();
            hasManaStoneCount--;
        }
        
        yield return new WaitForSeconds(1f);
        flashEffect.Stop();
        flashCooldownEffect.Play();
        Flashlight.gameObject.SetActive(false);

        yield return new WaitForSeconds(flashCooldown);
        possibleToFlash = true;
        flashCooldownEffect.Stop();
    }

    IEnumerator FootStepStart()
    {
        yield return new WaitForSeconds(0.1f);

        _soundManager.PlaySFX("PlayerFootStep");

    }
    
    IEnumerator FootStepStop()
    {
        _soundManager.StopSFX("PlayerFootStep");
        yield return new WaitForSeconds(0.1f);
    }
    
    public void Statue_Mid()
    {
        midStatue = true;
    }

    public void Statue_Under()
    {
        underStatue = true;
    }

    public void Statue_Upper()
    {
        upperStatue = true;
    }
}

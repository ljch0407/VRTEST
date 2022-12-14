using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInfo : MonoBehaviour
{
    public int healthPoint;
    public int hasManaStoneCount;
    
    private float hasteCooldown = 5f;
    private float flashCooldown = 5f;
    private float pathFindCooldown = 10f;
    private float SeethroughCooldown = 10f;
    
    
    public Transform menuTransform;
    public Transform playerLocationBefore;


    public bool isPaused = false;
    public bool possibleToHide = false;
    
    private bool possibleToHaste = true;
    private bool possibleToFlash = true;
    private bool possibleToPathFind = true;
    private bool possibleTpSeethrough = true;
    
    private InputDeviceCharacteristics rightControllerCharacteristics;
    private InputDeviceCharacteristics leftControllerCharacteristics;
    private InputDevice targetDevice;
    private InputDevice targetDeviceR;
    private bool menuButton;
    private bool hide;
    private bool haste;
    private bool flash;
    private bool seethrough;
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

    public ParticleSystem[] manaStoneUIs;

    public Camera mainCam;
    public Camera outlineCam;


    private bool Invincible;
    private bool Infinite_Mana;
    private bool MetaUISetting;
    
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        hasteEffect.Stop();
        flashEffect.Stop();
        hasteCooldownEffect.Stop();
        flashCooldownEffect.Stop();
      
        mainCam.enabled = true;
        outlineCam.enabled = false;

        Invincible = false;
        Infinite_Mana = false;
        MetaUISetting = false;
        
        foreach (var VARIABLE in manaStoneUIs)
        {
            VARIABLE.Stop();
        }
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
        if (healthPoint > 2)
        {
            GameObject postProcess = GameObject.Find("Post-process Volume Player");
            PostProcessVolume Volume = postProcess.GetComponent<PostProcessVolume>();
            Volume.enabled = false;
        }

        if (hasManaStoneCount == 0)
        {
            possibleToHaste = false;
            possibleToFlash = false;
            possibleToPathFind = false;
            possibleTpSeethrough = false;
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
        
        if (targetDeviceR.TryGetFeatureValue(CommonUsages.secondaryButton, out seethrough) && seethrough)
        {
            //Hide spot use
            Debug.Log("Right secondary Pressed");
            if (possibleTpSeethrough)
            {
                StartCoroutine(Seethrough());
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

        for (int i = 0; i < 5; ++i)
        {
            if (hasManaStoneCount > i)
            {
                manaStoneUIs[i].Play();
            }
            else
            {
                manaStoneUIs[i].Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(PlayerInvincible());
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            StartCoroutine(PlayerInfiniteMana());
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            StartCoroutine(PlayerMetaUISetting());
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
            StartCoroutine(HasteSound());
            hasteEffect.Play();
            continuousMoveProviderBase.moveSpeed = 4;
            hasManaStoneCount--;
        }
        
        yield return new WaitForSeconds(15f);
        continuousMoveProviderBase.moveSpeed = 2;
        hasteEffect.Stop();
        hasteCooldownEffect.Play();
        StartCoroutine(BreathLoop());
        
        yield return new WaitForSeconds(hasteCooldown);
        
        possibleToHaste = true;
        hasteCooldownEffect.Stop();
    }

    IEnumerator HasteSound()
    {
        _soundManager.PlaySFX("SFX_Haste");
        yield return new WaitForSeconds(2.0f);
        _soundManager.StopSFX("SFX_Haste");
    }

    IEnumerator BreathLoop()
    {
        _soundManager.StopSFX("SFX_Breath_loop");
        yield return new WaitForSeconds(5.0f);
        _soundManager.StopSFX("SFX_Breath_loop");
    }

    IEnumerator Seethrough()
    {
        if (possibleTpSeethrough)
        {
            possibleTpSeethrough = false;
            Debug.Log("Seethrough On");
            outlineCam.enabled = true;
            mainCam.enabled = false;
            hasManaStoneCount--;
        }
        
        yield return new WaitForSeconds(10f);
        outlineCam.enabled = false;
        mainCam.enabled = true;
        
        yield return new WaitForSeconds(SeethroughCooldown);
        possibleTpSeethrough = true;
    }
    
    IEnumerator Flash()
    {
        if (possibleToFlash)
        {
            possibleToFlash = false;
            Debug.Log("Flash On");
            StartCoroutine(FlashSound());
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
    
    IEnumerator FlashSound()
    {
        _soundManager.PlaySFX("SFX_Flash");
        yield return new WaitForSeconds(2.0f);
        _soundManager.StopSFX("SFX_Flash");
    }

    IEnumerator FootStepStart()
    {
        yield return new WaitForSeconds(0.1f);
        _soundManager.PlaySFX("SFX_FootStep");
    }
    
    IEnumerator FootStepStop()
    {
        _soundManager.StopSFX("SFX_FootStep");
        yield return new WaitForSeconds(0.1f);
    }
    
    IEnumerator PlayerInvincible()
    {
        Invincible = !Invincible;
        yield return new WaitForSeconds(0.1f);
        if (Invincible)
        {
           Debug.Log("POWEROVERWHELMING ACTIVATED");
           tag = "Invincible";
        }else if (!Invincible)
        {
            Debug.Log("POWEROVERWHELMING DEACTIVATED");
            tag = "Player";
        }
    }
    
    IEnumerator PlayerInfiniteMana()
    {
        Infinite_Mana = !Infinite_Mana;
        yield return new WaitForSeconds(0.1f);
        if (Infinite_Mana)
        {
            Debug.Log("INFINITE MANA ACTIVATED");
            hasManaStoneCount = 99;
        }else if (!Infinite_Mana)
        {
            Debug.Log("INFINITE MANA DEACTIVATED");
            hasManaStoneCount = 2;
        }
    }

    
    IEnumerator PlayerMetaUISetting()
    {
        MetaUISetting = !MetaUISetting;
        yield return new WaitForSeconds(0.1f);
        if (MetaUISetting)
        {
            Debug.Log("MetaUI Setting ACTIVATED");
            healthPoint = 1;
        }else if (!MetaUISetting)
        {
            Debug.Log("MetaUI Setting DEACTIVATED");
            healthPoint = 2;
        }
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

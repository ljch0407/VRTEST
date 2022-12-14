using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameStartObject : MonoBehaviour
{
    public XRGrabInteractable m_InteractableBase;

    public Transform m_TeleportLocation;
    public Transform gameStartObjectTransform;
    private Transform m_postPlayerTransform;
    private GameObject m_target;
    private bool m_TriggerDown = false;
    public SoundManager _soundManager;

    private void Start()
    {
        m_target = GameObject.FindWithTag("Player");
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.selectExited.AddListener(DropObject);
        m_InteractableBase.deactivated.AddListener(TriggerReleased);
        m_InteractableBase.activated.AddListener(TriggerPulled);

    }

    private void Update()
    {
        if (m_TriggerDown)
        {
            m_target.transform.position = m_TeleportLocation.transform.position;
            m_TriggerDown = false;
        }
    }

    public void DropObject(SelectExitEventArgs args)
    {
        m_TriggerDown = false;
        
        transform.position = gameStartObjectTransform.position;
        transform.rotation = gameStartObjectTransform.rotation;

    }
    public void TriggerReleased(DeactivateEventArgs args)
    {
        m_TriggerDown = false;
    }
    public void TriggerPulled(ActivateEventArgs args)
    {
        m_TeleportLocation.position = m_target.gameObject.GetComponent<PlayerInfo>().playerLocationBefore.position;
        _soundManager.StopBGM();
        _soundManager.PlayBGM("BGM1");
        m_target.GetComponent<PlayerInfo>().isPaused = false;
        
        Debug.Log("Triigerpulled");
        m_TriggerDown = true;
    }
}

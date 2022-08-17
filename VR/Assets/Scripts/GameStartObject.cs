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
    private Transform m_postPlayerTransform;
    private GameObject m_target;
    private bool m_TriggerDown = false;

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
        }
    }

    void DropObject(SelectExitEventArgs args)
    {
        m_TriggerDown = false;
    }
    void TriggerReleased(DeactivateEventArgs args)
    {
        m_TriggerDown = false;
    }
    void TriggerPulled(ActivateEventArgs args)
    {
        m_TeleportLocation.position = m_target.gameObject.GetComponent<PlayerInfo>().playerLocationBefore.position;
        Debug.Log("Triigerpulled");
        m_TriggerDown = true;
    }
}
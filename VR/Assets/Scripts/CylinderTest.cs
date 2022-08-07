using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]

public class CylinderTest : MonoBehaviour
{
    public XRGrabInteractable m_InteractableBase;
    private Transform m_Transform;
    
    private const float kHeldThreshold = 0.1f;

    private float m_TriggerHeldTime;
    private bool m_TriggerDown;
    
    void Start()
    {

        m_Transform = GetComponent<Transform>();
        m_InteractableBase.selectExited.AddListener(DropCylinder);
        m_InteractableBase.activated.AddListener(TriggerPulled);
        m_InteractableBase.deactivated.AddListener(TriggerReleased);
    }

    void Update()
    {
        if (m_TriggerDown)
        {
            m_TriggerHeldTime += Time.deltaTime;
            if (m_TriggerHeldTime >= kHeldThreshold)
            {
                transform.Rotate(new Vector3(0,1,0)*180 * Time.deltaTime);
            }
        }
    }

    void TriggerReleased(DeactivateEventArgs args)
    {
        m_TriggerDown = false;
        m_TriggerHeldTime = 0;
        
    }
    
    void TriggerPulled(ActivateEventArgs args)
    {
        Debug.Log("Triigerpulled");
        m_TriggerDown = true;
    }


    
    void DropCylinder(SelectExitEventArgs args)
    {
        m_TriggerDown = false;
        m_TriggerHeldTime = 0;
    }
}

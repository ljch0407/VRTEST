using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lantern : MonoBehaviour
{

    public Animator animator;
    public Light spotLight;
    public Light pointLight;
    public XRGrabInteractable interactableBase;
    
    public bool isopen = true;
    public bool dropDown = false;
    public Transform limitTransform;
    private float _floating = 0.0f;
    private float _limit;
    
    void Start()
    {
        animator.SetBool("IsOpen",true);
        interactableBase = GetComponent<XRGrabInteractable>();
        interactableBase.selectExited.AddListener(DropObject);
        interactableBase.selectEntered.AddListener(SelectObject);
        interactableBase.deactivated.AddListener(TriggerReleased);
        interactableBase.activated.AddListener(TriggerPulled);
        
    }

    // Update is called once per frame


    public void SelectObject(SelectEnterEventArgs args)
    {
        dropDown = false;
    }

    public void DropObject(SelectExitEventArgs args)
    {
        isopen = true;
        spotLight.enabled = false;
        pointLight.enabled = true;
        dropDown = true;
        _floating = _limit - 1.0f;
        animator.SetBool("IsOpen",true);
    }
    public void TriggerReleased(DeactivateEventArgs args)
    {
        isopen = true;
        spotLight.enabled = false;
        pointLight.enabled = true;
       
        dropDown = false;
        animator.SetBool("IsOpen",true);
    }
    public void TriggerPulled(ActivateEventArgs args)
    {
        isopen = false;
        animator.SetBool("IsOpen",false);
        spotLight.enabled = true;
        pointLight.enabled = false;
        
        dropDown = false;
        Debug.Log("Lantern Triigerpulled");
    }
    
}

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
    
    private bool isopen = false;
    void Start()
    {
        interactableBase = GetComponent<XRGrabInteractable>();
        interactableBase.selectExited.AddListener(DropObject);
        interactableBase.deactivated.AddListener(TriggerReleased);
        interactableBase.activated.AddListener(TriggerPulled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DropObject(SelectExitEventArgs args)
    {
        isopen = true;
        spotLight.enabled = false;
        pointLight.enabled = true;
        animator.SetBool("IsOpen",true);
    }
    public void TriggerReleased(DeactivateEventArgs args)
    {
        isopen = true;
        spotLight.enabled = false;
        pointLight.enabled = true;
        animator.SetBool("IsOpen",true);
    }
    public void TriggerPulled(ActivateEventArgs args)
    {
        isopen = false;
        animator.SetBool("IsOpen",false);
        spotLight.enabled = true;
        pointLight.enabled = false;
        Debug.Log("Lantern Triigerpulled");
    }
    
}

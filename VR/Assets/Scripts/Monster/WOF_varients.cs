using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WOF_varients : MonoBehaviour
{
    [SerializeField] Transform eyeBone;
    [SerializeField] Transform target;
    [SerializeField] float headMaxTurnAngle;
    [SerializeField] float headTrackingSpeed;
    
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        HeadTrackingUpdate();
    }
    
    void HeadTrackingUpdate()
    {
        //Head to Target
        Quaternion currentLocalRotation = eyeBone.localRotation;
        eyeBone.localRotation = Quaternion.identity;
        
        Vector3 targetWorldLookDir = target.position - eyeBone.position;
        Vector3 targetLocalLookDir = eyeBone.parent.InverseTransformDirection(targetWorldLookDir);
        
        
        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * headMaxTurnAngle,
            0);
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, -Vector3.up);
        
        eyeBone.localRotation = Quaternion.Slerp(currentLocalRotation,
            targetLocalRotation,
            1-Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Playables;

public class WallOfFlesh : MonoBehaviour
{
    [SerializeField] Transform eyeBone;
    [SerializeField] Transform eyeBone2;
    [SerializeField] Transform target;
    [SerializeField] float headMaxTurnAngle;
    [SerializeField] float headTrackingSpeed;
    
    public GameObject MiniMonsterPrefab;
    public Transform[] SpawnPoint;
    public Transform SpawnTransform;
    public PlayableDirector TimeDirector;
    
    
    private float monsterCounter;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        monsterCounter = 50;
        StartCoroutine(SpawnMonster());
    }

    // Update is called once per frame
    void Update()
    {
        HeadTrackingUpdate();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosive")
        {
            TimeDirector.Play();
        }
    }

    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(1f);

        if (Vector3.Distance(SpawnTransform.position, target.position) < 5)
        {
            monsterCounter--;
            
            if(monsterCounter>0) 
                Instantiate(MiniMonsterPrefab, SpawnPoint[0].position, SpawnPoint[0].rotation);
        }
       
        yield return new WaitForSeconds(3f);
        
        StartCoroutine(SpawnMonster());
       
    }
    void HeadTrackingUpdate()
    {
        //Head to Target
        Quaternion currentLocalRotation = eyeBone.localRotation;
        Quaternion currentLocalRotation2 = eyeBone.localRotation;
        eyeBone.localRotation = Quaternion.identity;
        
        Vector3 targetWorldLookDir = target.position - eyeBone.position;
        Vector3 targetLocalLookDir = eyeBone.parent.InverseTransformDirection(targetWorldLookDir);
        
        Vector3 targetWorldLookDir2 = target.position - eyeBone2.position;
        Vector3 targetLocalLookDir2 = eyeBone2.parent.InverseTransformDirection(targetWorldLookDir2);
        
        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * headMaxTurnAngle,
            0);
        targetLocalLookDir2 = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir2,
            Mathf.Deg2Rad * headMaxTurnAngle,
            0);
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, -Vector3.up);
        Quaternion targetLocalRotation2 = Quaternion.LookRotation(targetLocalLookDir2, -Vector3.up);
        
        eyeBone.localRotation = Quaternion.Slerp(currentLocalRotation,
            targetLocalRotation,
            1-Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );        
        eyeBone2.localRotation = Quaternion.Slerp(currentLocalRotation2,
            targetLocalRotation2,
            1-Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class WallOfFlesh : MonoBehaviour
{

    public GameObject MiniMonsterPrefab;
    public Transform[] SpawnPoint;
    public Transform targetTransform;
    
    
    private float monsterCounter;
    void Start()
    {
        monsterCounter = 100;
        StartCoroutine(SpawnMonster());
    }

    // Update is called once per frame
    void Update()
    {
    }

   

    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(1f);

        if (Vector3.Distance(targetTransform.position, transform.position) < 10)
        {
            monsterCounter--;
            
            if(monsterCounter>0) 
                Instantiate(MiniMonsterPrefab, SpawnPoint[0].position, SpawnPoint[0].rotation);
        }
       
        yield return new WaitForSeconds(3f);
        
        StartCoroutine(SpawnMonster());
       
    }
}
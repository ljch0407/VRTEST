using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class WanderingSpots : MonoBehaviour
{

    public Transform[] wanderingSpotTransforms;
    private List<Transform> WanderingSpotList;
    
    private Random rand = new Random();
    void Start()
    {
        WanderingSpotList = new List<Transform>();

        foreach (var VARIABLE in wanderingSpotTransforms)
        {
            WanderingSpotList.Add(VARIABLE);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            int result = rand.Next(0, WanderingSpotList.Count);
            other.GetComponent<Monster>().WanderingSpot = WanderingSpotList[result];
        }
    }
}

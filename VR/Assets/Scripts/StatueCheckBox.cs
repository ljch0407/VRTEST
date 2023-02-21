using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class StatueCheckBox : MonoBehaviour
{
    public GameObject Statue;
    private bool StatueAssembled;

    private void Awake()
    {
        StatueAssembled = false;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -300)
            StatueAssembled = false;
        
        if (StatueAssembled)
        {
            transform.position += Vector3.down * Time.deltaTime * 1;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInfo playerInfo = other.gameObject.GetComponent<PlayerInfo>();

            if (playerInfo.upperStatue && playerInfo.midStatue && playerInfo.underStatue)
            {
                Statue.SetActive(true);
                StatueAssembled = true;
            }
        }

    }
}

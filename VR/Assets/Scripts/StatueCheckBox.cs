using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCheckBox : MonoBehaviour
{
    public GameObject Statue;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInfo playerInfo = other.gameObject.GetComponent<PlayerInfo>();

            if (playerInfo.upperStatue && playerInfo.midStatue && playerInfo.underStatue)
            {
                Statue.SetActive(true);
            }
        }

    }
}

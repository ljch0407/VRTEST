using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class StatueCheckBox : MonoBehaviour
{
    public GameObject Statue;
    private bool StatueAssembled;

    private bool Statue_Visible;
    private void Awake()
    {
        StatueAssembled = false;
        Statue_Visible = false;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -300)
            StatueAssembled = false;
        
        if (StatueAssembled)
        {
            StartCoroutine(GoDown());
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

    IEnumerator GoDown()
    {
        if (!Statue_Visible)
        {
            yield return  new WaitForSeconds(2f);
            Statue_Visible = true;
        }

        
        transform.position += Vector3.down * Time.deltaTime * 1;
        yield return null;
    }
}

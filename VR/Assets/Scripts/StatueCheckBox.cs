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

    public float Speed = 1;
    //public ParticleSystem DustEffect;
    
    private void Awake()
    {
        StatueAssembled = false;
        Statue_Visible = false;
       // DustEffect.Stop();
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
           // DustEffect.Play();
            yield return  new WaitForSeconds(2f);
            Statue_Visible = true;
        }

        
        transform.position += Vector3.down * Time.deltaTime * Speed;
        yield return null;
    }
}

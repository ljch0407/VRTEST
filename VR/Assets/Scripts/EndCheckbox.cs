using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheckbox : MonoBehaviour
{
    public Transform resultPos;
    [SerializeField] GameObject[] EndCredit;
    private bool End = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !End)
        {
            other.transform.position = resultPos.position;
            for (int i = 0; i < EndCredit.Length; i++)
            {
                EndCredit[i].gameObject.SetActive(true);
            }
            End = true;
        }
    }
}

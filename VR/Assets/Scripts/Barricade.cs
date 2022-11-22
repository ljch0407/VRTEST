using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidBody;
    public SoundManager _soundManager;

    void Start()
    {
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>().instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hammer")
        {
            rigidBody.isKinematic = false;
           // StartCoroutine(BreakBarricades());
        }
    }

    IEnumerator BreakBarricades()
    {
        _soundManager.PlaySFX("BreakWoodenBarricades");
        yield return new WaitForSeconds(0.1f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody Other_rigidBody;
    public Rigidbody Door_rigidBody;
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
            Other_rigidBody.isKinematic = false;
            Door_rigidBody.constraints = RigidbodyConstraints.None;
            StartCoroutine(BreakBarricades());
        }
    }

    IEnumerator BreakBarricades()
    {
        _soundManager.PlaySFX("SFX_Barricade");
        yield return new WaitForSeconds(2f);
        _soundManager.StopSFX("SFX_Barricade");
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Barricade>().enabled = false;
    }
}

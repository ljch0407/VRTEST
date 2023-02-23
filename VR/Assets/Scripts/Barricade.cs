using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody Other_rigidBody;
    public Rigidbody Door_rigidBody;
    public AudioSource _audioSource;
    void Start()
    {
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
        //_soundManager.PlaySFX("SFX_Barricade");
        _audioSource.Play();
        yield return new WaitForSeconds(2f);
        Debug.Log("SFX_Barricade Played");
        //_soundManager.StopSFX("SFX_Barricade");
        _audioSource.Stop();
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Barricade>().enabled = false;
    }
}

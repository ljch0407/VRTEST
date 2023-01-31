using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public ParticleSystem Effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WOF" || other.tag == "Safe")
        {
            StartCoroutine(Explode(other));
        }
    }

    IEnumerator Explode(Collider other)
    {
        Effect.Play();
        yield return new WaitForSeconds(2);
        other.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        Effect.Stop();
    }
}

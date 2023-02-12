using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class Explosive : MonoBehaviour
{
    public ParticleSystem Effect;

    private void Awake()
    {
        Effect.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WOF" || other.tag == "Safe")
        {
            StartCoroutine(Explode(other));
        }
    }

    IEnumerator Explode(Collider other)
    {
        if (other.tag == "WOF")
        {
            Effect.Play();
            PlayableDirector Director = other.GetComponent<PlayableDirector>();
            Director.Play();
            yield return new WaitForSeconds(3);
            Effect.Stop();
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}

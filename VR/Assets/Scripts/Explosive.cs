using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class Explosive : MonoBehaviour
{
    public ParticleSystem Effect;
    public AudioSource _audioSource;
    private bool _isExploded=false;
    private void Awake()
    {
        Effect.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "WOF" || other.tag == "Safe") && !_isExploded)
        {
            StartCoroutine(Explode(other));
        }
    }

    IEnumerator Explode(Collider other)
    {
        if (other.tag == "WOF")
        {
            Effect.Play();
            _audioSource.Play();
            _isExploded = true;
            yield return new WaitForSeconds(3);
            Effect.Stop();
            _audioSource.Stop();

            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}

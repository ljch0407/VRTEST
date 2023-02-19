using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class MiniMonster : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Animator _anim;
    public Transform Target;

    public ParticleSystem Effect;
    
    private bool isAlive = true;
    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        isAlive = true;
    
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Effect.Stop();
        
        _anim.SetTrigger("Spawn");
    }


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MiniMonsterBehavior());
    }

    IEnumerator MiniMonsterBehavior()
    {
        yield return null;

        if (isAlive)
        {
            _anim.SetBool("Idle", false);
            _nav.stoppingDistance = 1.0f;
            _nav.SetDestination(Target.position);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            gameObject.GetComponent<MiniMonster>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            isAlive = false;
            _anim.SetBool("Idle", true);
            _anim.gameObject.SetActive(false);
        }
        else if (other.tag == "Player")
        {
            //Target.GetComponent<PlayerInfo>().healthPoint--;

            StartCoroutine(EffectPlay());
        }
    }

    IEnumerator EffectPlay()
    {
        Effect.Play();
        yield return new WaitForSeconds(0.4f);
        GameObject.Destroy(gameObject);
    }
}

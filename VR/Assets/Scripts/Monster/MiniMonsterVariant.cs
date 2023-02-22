using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class MiniMonsterVariant : MonoBehaviour
{
   private NavMeshAgent _nav;
    private Animator _anim;
    public Transform Target;
    public SoundManager _soundManager;
    public ParticleSystem Effect;
    public GameObject explosive;
    private bool isAlive = true;
    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        isAlive = true;
    
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        Effect.Stop();
        
        _anim.SetTrigger("Spawn");
        StartCoroutine(IDLESoundPlay());

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
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(DeadSoundPlay());
            gameObject.GetComponent<MiniMonster>().enabled = false;
            Instantiate(explosive,transform.position, transform.rotation);

        }
        else if (other.tag == "Player" && isAlive)
        {
            //Target.GetComponent<PlayerInfo>().healthPoint--;
            StartCoroutine(EffectPlay());
        }
    }

    IEnumerator EffectPlay()
    {
        Effect.Play();
        _soundManager.PlaySFX("SFX_MinMonster_Attack");
        isAlive = false;
        yield return new WaitForSeconds(0.4f);
        GameObject.Destroy(gameObject);
    }
    IEnumerator DeadSoundPlay()
    {
        _soundManager.PlaySFX("SFX_MinMonster_Dead");
        isAlive = false;
        gameObject.layer = 06;
        _anim.SetBool("Idle", true);
        _anim.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(0.4f);
    }
    IEnumerator IDLESoundPlay()
    {
        _soundManager.PlaySFX("SFX_MinMonster_IDLE");
        yield return new WaitForSeconds(0.4f);
    }
}

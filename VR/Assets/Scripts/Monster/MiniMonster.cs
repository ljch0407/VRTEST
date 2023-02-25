using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using UnityEngine.Experimental.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class MiniMonster : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Animator _anim;
    public Transform Target;
    public SoundManager _soundManager;
    public ParticleSystem Effect;
    private bool isAlive = true;

    public AudioSource _AudioSource;
    public ManagerAIScript _Ai;
    
    public int _id;

    private void Awake()
    {
        Effect.Stop();
    }
    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        isAlive = true;
    
        _Ai = GameObject.FindGameObjectWithTag("AiManager").GetComponent<ManagerAIScript>();
        _id = _Ai.monster_id_Update();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        _soundManager.Add_Monster_audio(_AudioSource, _id);
        
        
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
        if (other.tag == "Weapon" && other.gameObject.layer == 20)
        {
            isAlive = false;

            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            gameObject.layer = 06;
            
            StartCoroutine(DeadSoundPlay());


        }
        else if (other.tag == "Player" && isAlive)
        {
            //Target.GetComponent<PlayerInfo>().healthPoint--;
            isAlive = false;

            StartCoroutine(EffectPlay());
        }
    }

    IEnumerator EffectPlay()
    {
        _soundManager.Monster_StopSFX(_id);
        _soundManager.Monster_PlaySFX("SFX_MinMonster_Attack", _id);
        Effect.Play();

        yield return new WaitForSeconds(0.6f);
        GameObject.Destroy(gameObject);
    }
    IEnumerator DeadSoundPlay()
    {
        _soundManager.Monster_StopSFX(_id);

        _soundManager.Monster_PlaySFX("SFX_MinMonster_Dead", _id);
        yield return new WaitForSeconds(0.6f);
        _soundManager.Monster_StopSFX(_id);
        
        _anim.SetBool("Idle", true);
        _anim.enabled = false;
        
        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(gameObject);

    }
    IEnumerator IDLESoundPlay()
    {
        _soundManager.Monster_PlaySFX("SFX_MinMonster_IDLE", _id);
        yield return new WaitForSeconds(6f);
    }
}

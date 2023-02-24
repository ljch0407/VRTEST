using System;
using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

public enum MonsterState
{
    Idle,
    Chase,
    Attack,
    Wandering,
    Blind
}

public class Monster : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;
    private float AttackCooldown =0.0f;
    
    
    public Transform target;
    public MonsterState CurrentState = MonsterState.Idle;
    
    public int _id;

    public Transform WanderingSpot;

    [SerializeField]
    public bool isBlind = false;

    public PlayerInfo PlayerInfo;

    public SoundManager _soundManager;

    public AudioSource _AudioSource;
    public ManagerAIScript _Ai;

    // Start is called before the first frame update
    void Start()
    {
        _Ai = GameObject.FindGameObjectWithTag("AiManager").GetComponent<ManagerAIScript>();
        _id = _Ai.monster_id_Update();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>().instance;
        _soundManager.Add_Monster_audio(_AudioSource, _id);

        StartCoroutine(FSM());
    }


    protected virtual IEnumerator FSM()
    {
        yield return null;
        while (true)
        {
            yield return StartCoroutine(CurrentState.ToString());
        }
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;

        _soundManager.Monster_StopSFX(_id);
        _soundManager.Monster_PlaySFX("SFX_MainMonster_Idle",_id);

        anim.SetBool("Move",false);

        nav.speed = 1.0f;
        
        if (isBlind)
        {        
            yield return new WaitForSeconds(3.0f);
            isBlind = false;
        }
        else
            yield return new WaitForSeconds(1.0f);
        
        CurrentState = MonsterState.Wandering;
    }

    protected virtual IEnumerator Wandering()
    {
        yield return null;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("wandering"))
        {
            anim.SetTrigger("wandering");
        }
        nav.stoppingDistance = 0.0f;
        anim.SetBool("Move",true);
        nav.SetDestination(WanderingSpot.position);
    }
    
    protected virtual IEnumerator Chase()
    {
        yield return null;
        
        anim.SetBool("Move",true);

        nav.speed = 1.5f;
        nav.stoppingDistance = 0.2f;
        nav.SetDestination(target.position);
    }

    // IEnumerator StartHeartBeat()
    // {
    //     _soundManager.PlaySFX("SFX_Heartbeat");
    //     yield return new WaitForSeconds(10.0f);
    //     _soundManager.StopSFX("SFX_Heartbeat");
    // }

    protected virtual IEnumerator Attack()
    {
        yield return null;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
        }

        nav.stoppingDistance = 2.0f;
        nav.SetDestination(target.position);
        
        
            _soundManager.Monster_StopSFX(_id);
            _soundManager.Monster_PlaySFX("SFX_MainMonster_Bite", _id);
            _soundManager.PlaySFX("SFX_Hurt");

            GameObject postProcess = GameObject.Find("Post-process Volume Player");
            PostProcessVolume Volume = postProcess.GetComponent<PostProcessVolume>();
            Volume.enabled = true;

            yield return new WaitForSeconds(1.0f);
            _soundManager.Monster_StopSFX(_id);
            _soundManager.StopSFX("SFX_Hurt");
            
            anim.SetTrigger("Attack");

            nav.stoppingDistance = 2.0f;

            PlayerInfo.healthPoint--;
            CurrentState = MonsterState.Chase;
    }
    
    
    protected virtual IEnumerator Blind()
    {
        yield return null;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Blind"))
        {
            anim.SetTrigger("Blind");
        }

        anim.SetTrigger("Blind");
        isBlind = true;
        CurrentState = MonsterState.Idle;
        yield return new WaitForSeconds(2.0f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isBlind)
            {
                if (CurrentState == MonsterState.Idle)
                {
                    CurrentState = MonsterState.Chase;
                }
                else if (CurrentState == MonsterState.Wandering)
                {
                    CurrentState = MonsterState.Chase;
                }
                else if (CurrentState == MonsterState.Chase)
                {
                    CurrentState = MonsterState.Attack;
                }
                else if (CurrentState == MonsterState.Attack)
                {
                    CurrentState = MonsterState.Chase;
                }
            }
            else if (isBlind)
            {
                CurrentState = MonsterState.Idle;
            }
            
            if (other.tag == "PlayerHide")
            {
                CurrentState = MonsterState.Idle;
            }
        }
    }

   
}




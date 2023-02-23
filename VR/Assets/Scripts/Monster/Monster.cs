using System;
using System.Collections;
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

    public Transform target;
    public MonsterState CurrentState = MonsterState.Idle;

    private float blindedTime;
    public int _id;

    public Transform WanderingSpot;

    [SerializeField]
    public SphereCollider MeleeArea;
    public MeshCollider SightArea;
    public CapsuleCollider HearingArea;

    public bool isBlind = false;

    private float AtkCooldownMax = 3.0f;
    private float AtkCooldown = 0.0f;
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

        HearingArea.enabled = false;
        SightArea.enabled = true;
        MeleeArea.enabled = false;

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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("Idle");
        }
        
        anim.SetBool("Move",false);
        anim.SetBool("Chase",false);
        
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
        anim.SetBool("Move",true);
        nav.SetDestination(WanderingSpot.position);
    }
    
    protected virtual IEnumerator Chase()
    {
        yield return null;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("chase"))
        {
            anim.SetTrigger("chase");
        }

        StartCoroutine(StartHeartBeat());
        anim.SetBool("Move",true);
        anim.SetBool("Chase",true);
        
        nav.speed = 1.5f;
        nav.SetDestination(target.position);
    }

    IEnumerator StartHeartBeat()
    {
        _soundManager.PlaySFX("SFX_Heartbeat");
        yield return new WaitForSeconds(10.0f);
        _soundManager.StopSFX("SFX_Heartbeat");
    }

    protected virtual IEnumerator Attack()
    {
        yield return null;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
        }

        StartCoroutine(AttackSound());
        
        anim.SetTrigger("Attack");
        nav.stoppingDistance = 2.0f;
        AtkCooldown = 0.0f;
            
        yield return new WaitForSeconds(2.0f);
        PlayerInfo.healthPoint--;
        CurrentState = MonsterState.Idle;
    }
    
    IEnumerator AttackSound()
    {
        _soundManager.Monster_PlaySFX("SFX_Bite", 0);
        _soundManager.PlaySFX("SFX_Hurt");
        
        GameObject postProcess = GameObject.Find("Post-process Volume Player");
        PostProcessVolume Volume = postProcess.GetComponent<PostProcessVolume>();
        Volume.enabled = true;
        
        yield return new WaitForSeconds(2.0f);
        _soundManager.Monster_PlaySFX("SFX_Bite", 0);
        _soundManager.PlaySFX("SFX_Hurt");
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
        
        yield return new WaitForSeconds(2.0f);
        CurrentState = MonsterState.Idle;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isBlind)
            {
                if (CurrentState == MonsterState.Idle)
                {
                    HearingArea.enabled = false;
                    SightArea.enabled = true;
                    MeleeArea.enabled = false;
                    CurrentState =MonsterState.Chase;
                }
                else if (CurrentState == MonsterState.Wandering)
                {
                    HearingArea.enabled = false;
                    SightArea.enabled = true;
                    MeleeArea.enabled = false;
                    CurrentState = MonsterState.Chase;
                }
                else if (CurrentState == MonsterState.Chase)
                {
                    HearingArea.enabled = false;
                    SightArea.enabled = false;
                    MeleeArea.enabled = true;
                    CurrentState = MonsterState.Attack;
                }
                else if (CurrentState == MonsterState.Attack)
                {
                    HearingArea.enabled = false;
                    SightArea.enabled = true;
                    MeleeArea.enabled = false;
                }
            }
            else if (isBlind)
            {
                if (CurrentState == MonsterState.Idle)
                {
                    HearingArea.enabled = true;
                    SightArea.enabled = false;
                    MeleeArea.enabled = false;
                    CurrentState = MonsterState.Chase;
                }
                else if (CurrentState == MonsterState.Wandering)
                {
                    HearingArea.enabled = true;
                    SightArea.enabled = false;
                    MeleeArea.enabled = false;
                    CurrentState = MonsterState.Chase;
                }
                else if (CurrentState == MonsterState.Chase)
                {
                    HearingArea.enabled = false;
                    SightArea.enabled = false;
                    MeleeArea.enabled = true;
                    CurrentState = MonsterState.Attack;
                }
                else if (CurrentState == MonsterState.Attack)
                {
                    HearingArea.enabled = true;
                    SightArea.enabled = false;
                    MeleeArea.enabled = false;
                    CurrentState = MonsterState.Idle;
                }
            }
            
            if (other.tag == "PlayerHide")
            {
                HearingArea.enabled = false;
                SightArea.enabled = true;
                MeleeArea.enabled = false;
                CurrentState = MonsterState.Wandering;
            }
        }
    }
    
     private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CurrentState = MonsterState.Idle;
        }
    }
}




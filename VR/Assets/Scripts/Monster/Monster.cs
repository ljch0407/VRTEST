using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR.Haptics;

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

    public Transform WanderingSpot;

    [SerializeField]
    public SphereCollider MeleeArea;
    public MeshCollider SightArea;
    public CapsuleCollider HearingArea;

    public bool isBlind = false;

    private float AtkCooldownMax = 3.0f;
    private float AtkCooldown = 0.0f;
    public PlayerInfo PlayerInfo;

    // Start is called before the first frame update
    void Awake()
    {

        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();


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

        
        anim.SetBool("Move",true);
        anim.SetBool("Chase",true);
        
        nav.speed = 1.5f;
        nav.SetDestination(target.position);
    }

    protected virtual IEnumerator Attack()
    {
        yield return null;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
        }

        anim.SetTrigger("Attack");
        nav.stoppingDistance = 2.0f;
        AtkCooldown = 0.0f;
            
        yield return new WaitForSeconds(5.0f);
        PlayerInfo.healthPoint--;
        CurrentState = MonsterState.Idle;
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




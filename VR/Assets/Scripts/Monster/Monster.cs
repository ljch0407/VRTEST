using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{
    Idle,
    Chase,
    Attack,
    Wandering
}

public class Monster : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;
    
    public Transform target;
    public MonsterState State;

    private float blindedTime;

    public Transform WanderingSpot;
    
    [SerializeField]  
    public SphereCollider MeleeArea;
    public MeshCollider SightArea;
    public CapsuleCollider HearingArea;

    public bool isBlind = false;

    // Start is called before the first frame update
    void Awake()
    {
       
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        State = MonsterState.Idle;
        blindedTime = 0.0f;
        
        HearingArea.enabled = false;
        SightArea.enabled = true;
        MeleeArea.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (State == MonsterState.Idle)
        {
            StartCoroutine(Idle());
        }
        else if (State == MonsterState.Wandering)
        {
            StartCoroutine(Wandering());
        }
        else if (State == MonsterState.Chase)
        {
            StartCoroutine(Chase());
        }
        else if (State == MonsterState.Attack)
        {
            StartCoroutine(Attack());
        }


        if (isBlind)
        {
            StartCoroutine(Blind());
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerHide")
        {
            State = MonsterState.Idle;
            target = null;
        }
        else
        {
             if (State == MonsterState.Idle)
                    {
                        if (!isBlind)
                        {
                            HearingArea.enabled = false;
                            SightArea.enabled = true; 
                            MeleeArea.enabled = false;
                        }else if (isBlind)
                        {
                            HearingArea.enabled = true;
                            SightArea.enabled = false; 
                            MeleeArea.enabled = false;
                        }
            
                        if (other.tag == "Player")
                        {
                            State = MonsterState.Chase;
                            target = other.transform;
                        }
            
                    }
                    else if (State == MonsterState.Chase)
                    {
                       
                        HearingArea.enabled = false;
                        SightArea.enabled = false;
                        MeleeArea.enabled = true;
            
                        if (other.tag == "Player")
                        {
                            State = MonsterState.Attack;
                            anim.SetTrigger("Attack");
                        }
                    }
                    else if (State == MonsterState.Wandering)
                    {
                        if (!isBlind)
                        {
                            HearingArea.enabled = false;
                            SightArea.enabled = true; 
                            MeleeArea.enabled = false;
                        }else if (isBlind)
                        {
                            HearingArea.enabled = true;
                            SightArea.enabled = false; 
                            MeleeArea.enabled = false;
                        }
                        if (other.tag == "Player")
                        {
                            State = MonsterState.Chase;
                            target = other.transform;
                        }
                    }
                    else if (State == MonsterState.Attack)
                    {
                        State = MonsterState.Idle;
                        anim.SetBool("Chase",false);
                        anim.SetBool("Move",false);

                    }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (State == MonsterState.Chase)
            {
                State = MonsterState.Idle;
                target = null;
                anim.SetBool("Chase", false);
                anim.SetBool("Move", false);
            }
        }
        else if (other.tag == "PlayerHide")
        {
            State = MonsterState.Idle;
            target = null;
            anim.SetBool("Chase", false);
            anim.SetBool("Move", false);
        }
    }
    
    
    IEnumerator Idle()
    {
        yield return null;
        
        State = MonsterState.Wandering;
        anim.SetBool("Move",true);
        anim.SetBool("Chase", false);
    }

    IEnumerator Wandering()
    {
        yield return null;
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            anim.SetTrigger("Move");
        }

        nav.SetDestination(WanderingSpot.position);
    }

    IEnumerator Chase()
    {
        yield return null;
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Chase"))
        {
            anim.SetTrigger("Chase");
        }   
        
        anim.SetBool("Chase", true);
        anim.SetBool("Move",false);
        nav.SetDestination(target.transform.position);
    }

    IEnumerator Attack()
    {
        yield return null;
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
        }   
        
        nav.SetDestination(target.transform.position);
        GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().healthPoint--;
        State = MonsterState.Idle;
        target = null;
    }

    IEnumerator Blind()
    {
        yield return null;
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Blind"))
        {
            anim.SetTrigger("Blind");
        }   

        blindedTime += Time.deltaTime;
        anim.SetTrigger("Blind");
        target = null;
        State = MonsterState.Idle;
        anim.SetBool("Chase",false);
        anim.SetBool("Move",false);
        if (blindedTime > 5.0f)
        {
            blindedTime = 0.0f;
            isBlind = false;
        }
    }
    
}




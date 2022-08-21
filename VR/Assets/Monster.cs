using System;
using System.Collections;
using System.Collections.Generic;
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

    public Transform wanderingSpot1;
    public Transform wanderingSpot2;

    public Transform target;
    public MonsterState State;

    private float blindedTime;

    public bool isBlind = false;
    // Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        State = MonsterState.Idle;
        blindedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == MonsterState.Idle)
        {
            State = MonsterState.Wandering;
            anim.SetBool("Move",true);
            nav.SetDestination(wanderingSpot1.position);
        }
        else if (State == MonsterState.Wandering)
        {
            if (transform.position.x == wanderingSpot1.position.x &&transform.position.z == wanderingSpot1.position.z )
            {
                nav.SetDestination(wanderingSpot2.position);
            }
            else if(transform.position.x == wanderingSpot2.position.x &&transform.position.z == wanderingSpot2.position.z)
            {
                nav.SetDestination(wanderingSpot1.position);
            }
        }
        else if (State == MonsterState.Chase)
        {
            anim.SetBool("Chase", true);
            nav.SetDestination(target.transform.position);
        }
        else if (State == MonsterState.Attack)
        {
            nav.SetDestination(target.transform.position);
            GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().healthPoint--;
        }


        if (isBlind)
        {
            blindedTime += Time.deltaTime;
            if (blindedTime > 10.0f)
            {
                blindedTime = 0.0f;
                isBlind = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            State = MonsterState.Attack;
            anim.SetTrigger("Attack");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            State = MonsterState.Idle;
            anim.SetBool("Chase",false);
        }
    }
}

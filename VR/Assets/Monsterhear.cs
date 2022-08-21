using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterhear : MonoBehaviour
{
    public Monster monster;

    private void Update()
    {
        if (!monster.isBlind)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            monster.State = MonsterState.Chase;
            monster.target.position = collisionInfo.transform.position;
        }
    }

    private void OnCollisionExit(Collision other)
    {

        if (other.gameObject.tag == "player")
        {
            monster.State = MonsterState.Wandering;
            monster.target = null;
        }
    }
}

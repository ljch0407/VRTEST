using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAIScript : MonoBehaviour
{
    public List<GameObject> Monsters;
    private PlayerInfo _playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        Monsters = new List<GameObject>();
        _playerInfo = GameObject.FindWithTag("Player").GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInfo.isPaused)
        {
            foreach (var monster in Monsters)
            {
                monster.SetActive(false);
                monster.gameObject.GetComponent<Monster>().State = MonsterState.Idle;
            }
        }
        else if(!_playerInfo.isPaused)
        {
            foreach (var monster in Monsters)
            {
                monster.SetActive(true);
                monster.gameObject.GetComponent<Monster>().State = MonsterState.Idle;
            }
        }

    }
}

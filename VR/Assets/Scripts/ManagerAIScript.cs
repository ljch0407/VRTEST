using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManagerAIScript : MonoBehaviour
{
    public List<GameObject> monstersList;

    public GameObject[] monsterPrefabs;

    private PlayerInfo _playerInfo;

    private int _number_of_id = 0;
    // Start is called before the first frame update
    void Start()
    {
        monstersList = new List<GameObject>();
        
        foreach (var VARIABLE in monsterPrefabs)
        {
            monstersList.Add(VARIABLE);
        }


        _playerInfo = GameObject.FindWithTag("Player").GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInfo.isPaused)
        {
            foreach (var m in monstersList)
            {
                m.GetComponent<Monster>().CurrentState = MonsterState.Idle;
                m.SetActive(false);
            }
           

        }
        else if(!_playerInfo.isPaused)
        {
            foreach (var m in monstersList)
            {
                m.SetActive(true);
            }
        }
    }

    public int monster_id_Update()
    {
        _number_of_id++;
        return _number_of_id;
    }
    
}

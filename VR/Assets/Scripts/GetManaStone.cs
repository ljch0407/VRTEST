using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetManaStone : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    void Start()
    {
        _playerInfo = GameObject.FindWithTag("Player").GetComponent<PlayerInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ManaStone")
        {
            other.gameObject.SetActive(false);
            _playerInfo.hasManaStoneCount++;
        }
    }
}

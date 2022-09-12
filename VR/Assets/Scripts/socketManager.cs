using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class socketManager : MonoBehaviour
{
    public SphereCollider under;
    public SphereCollider mid;
    public SphereCollider upper;

    private PlayerInfo _playerInfo;
    void Start()
    {
        mid.enabled = false;
        upper.enabled = false;
        under.enabled = false;

        _playerInfo = GameObject.FindWithTag("Player").GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInfo.underStatue)
        {
            under.enabled = true;
        }

        if (_playerInfo.midStatue)
        {
            mid.enabled = true;
        }

        if (_playerInfo.upperStatue)
        {
            upper.enabled = true;
        }
    }
}

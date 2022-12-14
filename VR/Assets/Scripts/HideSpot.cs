using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideSpot : MonoBehaviour
{
    private bool _isUsed = false;
    private GameObject _player;
    
    public float _coolTime = 0;

    public Transform playerLocation;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isUsed)
        {
            _coolTime += Time.deltaTime;

            if (_coolTime >= 7.0)
            {
                _player.transform.position = playerLocation.position;
                _player.transform.rotation = playerLocation.rotation;
                _player.tag = "Player";
                _player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                _coolTime = 0;
                _isUsed = false;
            }

        }
    }

    public void Hide()
    {
        _player.transform.position = transform.position;
        _player.transform.rotation = transform.rotation;
        _player.tag = "PlayerHide";
        _player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;

        _isUsed = true;
    }

}

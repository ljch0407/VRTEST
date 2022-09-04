using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideSpot : MonoBehaviour
{
    private bool _isUsed = false;
    private GameObject _player;
    
    private float _coolTimeLimit = 15f;
    private float _coolTime = 0;

    private Transform _playerLocation;
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

            if (_coolTime == 5.0)
            {
                _player.transform.position = _playerLocation.position;
                _player.transform.rotation = _playerLocation.rotation;
                _player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            }

            if (_coolTime == _coolTimeLimit)
            {
                _coolTime = 0;
                _isUsed = false;
            }
        }
    }

    public void Hide()
    {
        _playerLocation.position = _player.transform.position;
        _playerLocation.rotation = _player.transform.rotation;
        
        _player.transform.position = transform.position;
        _player.transform.rotation = transform.rotation;

        _player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;

        _isUsed = true;
    }

}

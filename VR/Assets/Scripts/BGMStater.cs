using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStater : MonoBehaviour
{
    public SoundManager _soundManager;

    [SerializeField] WallOfFlesh[] _WOFs;
    void Start()
    {
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>().instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _soundManager.PlayBGM("BGM3_BOSS");
            for (int i = 0; i < _WOFs.Length ; i++)
            {
                _WOFs[i].MobSpawnTime = 4f;
                _WOFs[i].MobSpawnDistance = 20f;
            }
        }
    }
}

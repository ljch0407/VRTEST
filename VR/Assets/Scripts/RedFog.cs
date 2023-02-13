using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFog : MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    public bool FirstFog;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstFog)
        {
            if (PlayerInfo.underStatue)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (PlayerInfo.midStatue)
            {
                gameObject.SetActive(false);
            }
        }

    }
}

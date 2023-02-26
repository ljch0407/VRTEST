using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RedFogState
{
    Mid,
    Under
    
}

public class RedFog : MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    public bool FogActivate = true;
    public RedFogState state;


    void FixedUpdate()
    {
        if (FogActivate)
        {
            if (PlayerInfo.underStatue && state == RedFogState.Under)
            {
                gameObject.SetActive(false);
            }
            else if (PlayerInfo.midStatue && state == RedFogState.Mid)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

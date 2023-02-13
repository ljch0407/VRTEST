using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFog : MonoBehaviour
{
    public PlayerInfo PlayerInfo; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInfo.underStatue)
        {
            gameObject.SetActive(false);
        }
    }
}

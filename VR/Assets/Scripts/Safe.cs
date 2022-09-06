using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{

    public bool isOpen = false;
    
    public List<GameObject> stoolList;
    public GameObject[] stoolPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        stoolList = new List<GameObject>();
        foreach (var VARIABLE in stoolPrefabs)
        {
            stoolList.Add(VARIABLE);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

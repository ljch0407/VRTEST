using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{

    public bool isOpen = false;
    
    private List<GameObject> _stoolList;
    public GameObject[] stoolPrefabs;
    public int countOfStoolActive;
    
    // Start is called before the first frame update
    void Start()
    {
        countOfStoolActive = 0;
        _stoolList = new List<GameObject>();
        foreach (var stoolObject in stoolPrefabs)
        {
            _stoolList.Add(stoolObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_stoolList[0].GetComponent<Stool>().isActive &&
            _stoolList[1].GetComponent<Stool>().isActive &&
            _stoolList[2].GetComponent<Stool>().isActive &&
            countOfStoolActive == 3)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }

    }
}

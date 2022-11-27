using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{

    public bool isOpen;
    
    private List<GameObject> _stoolList;
    public GameObject[] stoolPrefabs;
    public int countOfStoolActive;
    public GameObject Safedoor;

    private float doorRotate;
    
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        countOfStoolActive = 0;
        _stoolList = new List<GameObject>();
        foreach (var stoolObject in stoolPrefabs)
        {
            _stoolList.Add(stoolObject);
        }

        doorRotate = 180f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_stoolList[0].GetComponent<Stool>().isActive)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }

        if (isOpen)
        {
            Safedoor.transform.Rotate(new Vector3(0,0,-1) * doorRotate * Time.deltaTime);
            doorRotate -= 1f;
            if (doorRotate < 100)
                isOpen = false;
        }

    }
}

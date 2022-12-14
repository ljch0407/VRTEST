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
    void FixedUpdate()
    {
        
        if (_stoolList[0].GetComponent<Stool>().isActive)
        {
            isOpen = true;
            StartCoroutine(SafeOpen());
            
        }
        else
        {
            isOpen = false;
        }
    }

    IEnumerator SafeOpen()
    {
        if (doorRotate < 100)
            isOpen = false;
        yield return new WaitForSeconds(0.1f);
        
        if (isOpen)
        {
            Safedoor.transform.Rotate(new Vector3(0,0,-1) * doorRotate * Time.fixedDeltaTime);
            doorRotate -= 1f;
        }
    }
}

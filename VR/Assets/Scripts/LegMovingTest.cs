using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMovingTest : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed;
    public Transform Startpoint;

    public Transform Point1;
    public Transform Point2;
    
    public Transform Endpoint;
    
    
    // private bool moveForward = true;

    public float direction;
    void Start()
    {
        direction = 0;
        transform.position = Startpoint.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (direction == 0)
        {
            transform.position =Vector3.MoveTowards(transform.position, Point1.position, Time.deltaTime * Speed * 2);
            if (transform.position == Point1.position)
                direction = 1;
        }
        else if (direction == 1)
        {
            transform.position =Vector3.MoveTowards(transform.position, Point2.position, Time.deltaTime * Speed * 2);
            if (transform.position == Point2.position)
                direction = 2;
        }
        else if (direction == 2)
        {
            transform.position =Vector3.MoveTowards(transform.position, Endpoint.position, Time.deltaTime * Speed * 2);

            if (transform.position == Endpoint.position)
                direction = 3; 
        }
        else if (direction == 3)
        {
            transform.position =Vector3.MoveTowards(transform.position, Startpoint.position, Time.deltaTime * Speed * 2);

            if (transform.position == Startpoint.position)
                direction = 0; 
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    [SerializeField] LayerMask terrainLayer = default;
    [SerializeField] float delay;

    private Vector3 rayCastStartPos;
    void Start()
    {
        rayCastStartPos = transform.position + new Vector3(0, 30, 0);
    }

    void LateUpdate()
    {
        Ray ray = new Ray(rayCastStartPos, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 100, terrainLayer.value))
        {
            transform.position = new Vector3(transform.position.x, info.point.y + delay, transform.position.z);
        }
    }
}

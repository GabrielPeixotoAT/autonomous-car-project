using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltrassonicSensor : MonoBehaviour
{
    public LayerMask layerMask;
    public int distance;

    public float value;

    // Update is called once per frame
    void Update()
    {        
        if (Physics.Raycast(transform.position, Vector3.forward, distance, layerMask))
        {
            Debug.Log("Obstáculo");
            value = 0;
        }
        else 
        {
            value = distance;
        }
    }
}

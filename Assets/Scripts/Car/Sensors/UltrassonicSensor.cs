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
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward);
        if (Physics.Raycast(this.transform.position, this.transform.forward, distance, layerMask))
        {
            value = 0;
        }
        else 
        {
            value = distance;
        }
    }
}

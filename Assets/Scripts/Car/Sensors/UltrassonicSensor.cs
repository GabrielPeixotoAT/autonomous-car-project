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
        RaycastHit hit;

        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward);
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distance, layerMask))
        {
            Debug.DrawRay(this.transform.position, this.transform.forward * distance, Color.white);
            value = hit.distance;
        }
        else 
        {
            value = distance;
        }
    }
}

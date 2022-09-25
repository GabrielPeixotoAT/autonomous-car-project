using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBSensorModule : MonoBehaviour
{
    public LayerMask layerMask;

    public int reflection;

    // Update is called once per frame
    void Update()
    {        
        if (Physics.Raycast(transform.position, Vector3.down, 1, layerMask))
        {
            reflection = 0;
        }
        else 
        {
            reflection = 10;
        }
    }
}

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, layerMask))
        {
            if (hit.collider.gameObject.tag == "ArrowLine")
            {
                reflection = 7;
            }
            else if (hit.collider.gameObject.tag == "CarryLine")
            {
                reflection = 5;
            }
            else
            {
                reflection = 0;
            }
        }
        else 
        {
            reflection = 10;
        }
    }
}

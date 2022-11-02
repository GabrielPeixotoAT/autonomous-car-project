using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour
{
    public GameObject areaFunction;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            areaFunction.GetComponent<InteractFunction>().Execute();
        }
    }
}

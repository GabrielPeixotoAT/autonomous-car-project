using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject camera;
    public LayerMask layerMask;
    public float distance;

    public Animator CrosshairAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCross();
    }

    void UpdateCross()
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, distance, layerMask))
        {
            CrosshairAnimator.SetBool("Interact", true);
        }
        else 
        {
            CrosshairAnimator.SetBool("Interact", false);
        }
    }
}

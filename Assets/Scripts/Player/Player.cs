using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject camera;
    public LayerMask layerMask;
    public float distance;

    public Animator CrosshairAnimator;

    public bool InInteractArea;
    GameObject InteractAreaFunction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();
        UpdateCross();
    }

    void CheckKeys()
    {
        if (Input.GetKeyDown(KeyCode.E) && InInteractArea)
        {
            InteractAreaFunction.GetComponent<InteractFunction>().Execute();
        }
    }

    void UpdateCross()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance, layerMask))
        {
            CrosshairAnimator.SetBool("Interact", true);
            InInteractArea = true;
            InteractAreaFunction = hit.transform.gameObject;
        }
        else 
        {
            CrosshairAnimator.SetBool("Interact", false);
            InInteractArea = false;
            InteractAreaFunction = null;
        }
    }
}

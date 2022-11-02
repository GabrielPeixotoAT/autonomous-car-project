using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGate : MonoBehaviour, InteractFunction
{
    public Animator anim;

    bool isOpen;

    public void Execute()
    {
        isOpen = !isOpen;
        anim.SetBool("Open", isOpen);
    }
}

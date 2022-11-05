using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGate : MonoBehaviour, InteractFunction
{
    public Animator AnimatorGate, AnimatorButton;

    public bool IsActive;

    public void Execute()
    {   
        IsActive = !IsActive;
        AnimatorGate.SetBool("Open", IsActive);
        AnimatorButton.SetTrigger("Press");
    }
}

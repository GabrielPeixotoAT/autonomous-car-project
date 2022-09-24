using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] wheels;
    public WheelCollider[] colliders;
    
    float inputView;

    public float torque;

    void Start()
    {
        
    }

    void Update()
    {
        SetWheelsPosition();

        inputView = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.W))
        {
            FreeBrakeAll();
            AccelerateMotor(0,1);
            AccelerateMotor(2,3);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            FreeBrakeAll();
            AccelerateMotorReverse(0,1);
            AccelerateMotor(2,3);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            FreeBrakeAll();
            AccelerateMotorReverse(2,3);
            AccelerateMotor(0,1);
        }
        if (false)
        {
            StopCar();
        }
    }

    void StopCar()
    {
        for (int i = 0; i < 4; i++)
        {
            colliders[i].brakeTorque = torque;
        }
    }

    void AccelerateMotor(int wheel1, int wheel2)
    {
        colliders[wheel1].motorTorque = torque * (-1);
        colliders[wheel2].motorTorque = torque * (-1);
    }

    void AccelerateMotorReverse(int wheel1, int wheel2)
    {
        colliders[wheel1].motorTorque = torque;
        colliders[wheel2].motorTorque = torque;
    }


    void BrakeMotor(int wheel1, int wheel2)
    {
        colliders[wheel1].brakeTorque = torque/2;
        colliders[wheel2].brakeTorque = torque/2;
    }

    void FreeBrake(int wheel1, int wheel2)
    {
        colliders[wheel1].brakeTorque = 0;
        colliders[wheel2].brakeTorque = 0;
    }

    void FreeBrakeAll()
    {
        for (int i = 0; i < 4; i++)
        {
            colliders[i].brakeTorque = 0;
        }
    }

    void SetWheelsPosition()
    {
        for (int i = 0; i < 4; i++)
        {
            wheels[i].transform.position = colliders[i].transform.position;
            wheels[i].transform.rotation = colliders[i].transform.rotation;
        }
    }
}

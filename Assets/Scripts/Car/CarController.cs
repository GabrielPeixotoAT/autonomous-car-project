using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] wheels;
    public WheelCollider[] colliders;
    public float speed, topSpeed, maneuveringSpeed;

    Vector3 pos;
    Quaternion rot;
    
    public float inputView, inputView2;

    public float torque;

    void Start()
    {
        
    }

    void Update()
    {   
        inputView = Input.GetAxis("Vertical");
        inputView2 = Input.GetAxis("Horizontal");

        SetSpeed();

        if (inputView != 0)
        {
            Forward();
        }
        else 
        {
            StopCar();
        }

        if (inputView2 > 0)
        {
            Turn(true);
        }
        else if (inputView2 < 0)
        {
            Turn(false);
        }
        else
        {

        }

        SetWheelsPosition();
    }

    void Forward()
    {
        if (speed < topSpeed)
        {
            FreeBrakeAll();
            AccelerateMotor(0,1);
            AccelerateMotor(2,3);
        }
        else 
        {
            NoAccelerate(0,1);
            NoAccelerate(2,3);
        }
    }

    void Turn(bool left)
    {
        FreeBrakeAll();

        if (left)
        {
            colliders[0].motorTorque = torque * (-1);
            colliders[1].motorTorque = torque * (-1);
            colliders[2].motorTorque = torque;
            colliders[3].motorTorque = torque;
        }
        else
        {
            colliders[0].motorTorque = torque;
            colliders[1].motorTorque = torque;
            colliders[2].motorTorque = torque * (-1);
            colliders[3].motorTorque = torque * (-1);
        }
    }

    void StopCar()
    {
        for (int i = 0; i < 4; i++)
        {
            colliders[i].brakeTorque = torque * 2;
        }
    }

    void SetSpeed()
    {
        speed = 1.57f * colliders[0].rpm;
        speed = Mathf.Abs(speed);
    }

    void AccelerateMotor(int wheel1, int wheel2)
    {
        colliders[wheel1].motorTorque = torque * (-1) * inputView;
        colliders[wheel2].motorTorque = torque * (-1) * inputView;
    }

    void NoAccelerate(int wheel1, int wheel2)
    {
        colliders[wheel1].motorTorque = 0;
        colliders[wheel2].motorTorque = 0;
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
            colliders[i].GetWorldPose(out pos, out rot);
            wheels[i].transform.position = pos;
            wheels[i].transform.rotation = rot;
        }
    }
}

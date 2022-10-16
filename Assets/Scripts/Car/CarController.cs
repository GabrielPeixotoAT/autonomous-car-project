using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] wheels;
    public WheelCollider[] colliders;
    public float speed, topSpeed, maneuveringSpeed;

    public RGBSensorModule[] RGBsensor;
    public UltrassonicSensor[] ultrassonicSensor;

    public Material materialLeft, materialRight;

    public Animator platformAnimator;

    float arrowTime, arrowTimeToOff;
    bool arrowOn, arrowNeeded, inManeuver;
    char dir;
    int maneuverState, maneuverIndex;

    Vector3 pos;
    Quaternion rot;
    
    public float inputView, inputView2;

    public float torque;

    void Start()
    {
        maneuverState = 0;
        maneuverIndex = 0;
    }

    void Update()
    {
        if (ultrassonicSensor[0].value != 0 && !inManeuver)
        {
            if (RGBsensor[0].reflection == 0)
            {
                //BrakeMotor(3,2);
                AccelerateMotorReverse(3,2);
                FreeBrake(0,1);
                AccelerateMotor(0,1);
            }
            else if(RGBsensor[0].reflection == 10)
            {
                FreeBrake(3,2);
                AccelerateMotor(3,2);
            }
            else if (RGBsensor[0].reflection == 5)
            {
                maneuverIndex = 1;
                inManeuver = true;
                StopCar();
            }

            if (RGBsensor[1].reflection == 0)
            {
                //BrakeMotor(0,1);
                AccelerateMotorReverse(0,1);
                FreeBrake(3,2);
                AccelerateMotor(3,2);
            }
            else if (RGBsensor[1].reflection == 10)
            {
                FreeBrake(0,1);
                AccelerateMotor(0,1);
            }
            else if (RGBsensor[1].reflection == 5)
            {
                maneuverIndex = 1;
                inManeuver = true;
                StopCar();
            }

            if(RGBsensor[0].reflection == 7)
            {
                arrowNeeded = true;
                arrowTimeToOff = Time.time + 5.5f;
                dir = 'R';
            }
            else if (RGBsensor[1].reflection == 7)
            {
                arrowNeeded = true;
                arrowTimeToOff = Time.time + 5.5f;
                dir = 'L';
            }

            if (arrowTimeToOff < Time.time)
            {
                arrowNeeded = false;
            }

            if (arrowTime < Time.time && (arrowNeeded || arrowOn))
            {
                arrowTime = Time.time + 0.5f;
                arrowOn = !arrowOn;
                SetArrow(dir, arrowOn);
            }
            
        }
        else if (inManeuver)
        {
            ManeuverManager(maneuverIndex);
        }
        else
        {
            StopCar();
        }
    }

    void Forward()
    {
        if (speed < topSpeed && speed > (topSpeed * (-1)))
        {
            FreeBrakeAll();
            AccelerateMotor(0,1);
            AccelerateMotor(3,2);
        }
        else 
        {
            NoAccelerate(0,1);
            NoAccelerate(3,2);
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

    void SetArrow(char dir, bool onOff)
    {
        if (onOff)
        {
            ArrowOn(dir);
        }
        else
        {
            ArrowOff(dir);
        }
    }

    void ArrowOn(char direction)
    {
        if (direction == 'L')
        {
            materialLeft.EnableKeyword("_EMISSION");
        }
        else if(direction == 'R')
        {
            materialRight.EnableKeyword("_EMISSION");
        }
    }

    void ArrowOff(char direction)
    {
        if (direction == 'L')
        {
            materialLeft.DisableKeyword("_EMISSION");
        }
        else if(direction == 'R')
        {
            materialRight.DisableKeyword("_EMISSION");
        }
    }

    void StopCar()
    {
        for (int i = 0; i < 4; i++)
        {
            colliders[i].brakeTorque = torque * 2;
        }
    }

    void SetSpeed(int index)
    {
        speed = 1.57f * colliders[index].rpm;
        speed = speed * (-1);
    }

    void AccelerateMotor(int wheel1, int wheel2)
    {
        SetSpeed(wheel1);

        if (speed < topSpeed)
        {
            colliders[wheel1].motorTorque = torque * (-1);
            colliders[wheel2].motorTorque = torque * (-1);
        }
        else 
        {
            NoAccelerate(wheel1, wheel2);
        }
    }

    void NoAccelerate(int wheel1, int wheel2)
    {
        colliders[wheel1].motorTorque = 0;
        colliders[wheel2].motorTorque = 0;
    }

    void NoAccelerateAll()
    {
        for (int i = 0; i < 4; i++)
        {
            colliders[i].motorTorque = 0;
        }
    }

    void AccelerateMotorReverse(int wheel1, int wheel2)
    {
        if(speed > (topSpeed * (-1)))
        {
            colliders[wheel1].motorTorque = torque;
            colliders[wheel2].motorTorque = torque;
        }
        else
        {
            NoAccelerateAll();
        }
    }

    void BrakeMotor(int wheel1, int wheel2)
    {
        colliders[wheel1].brakeTorque = torque * 5;
        colliders[wheel2].brakeTorque = torque * 5; 
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

    void ManeuverManager(int index)
    {
        switch (index)
        {
            case 1:
                PickUpLoad();
                break;
        }
    }

    public void ManeuverStateIncrement()
    {
        maneuverState++;
    }

    //Pick up load
    void PickUpLoad()
    {
        if (maneuverState == 0)
        {
            GearUp(platformAnimator);
        }
        else if (maneuverState == 1)
        {
            HalfTurn();
        }
    }

    void GearUp(Animator animator)
    {
        Debug.Log("UP!");
        animator.SetInteger("state", 1);
    }

    void HalfTurn()
    {
        AccelerateMotor(0,1);
        AccelerateMotorReverse(3,2);
    }

    void GearDown(Animator animator)
    {
        animator.SetInteger("state", 2);
    }
}

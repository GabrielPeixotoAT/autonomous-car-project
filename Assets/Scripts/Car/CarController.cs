using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool vehicleIsOn;
    public GameObject[] wheels;
    public WheelCollider[] colliders;
    public float speed, topSpeed, maneuveringSpeed;

    public RGBSensorModule[] RGBsensor;
    public UltrassonicSensor[] ultrassonicSensor;

    public Material materialLeft, materialRight;

    public Animator platformAnimator;

    bool forAuxTime, isStoped, isSoundPlay;
    float arrowTime, arrowTimeToOff, upTime, turnTime, perSecondTime;
    bool arrowOn, arrowNeeded, inManeuver;
    char dir;
    int maneuverState, maneuverIndex;

    Vector3 pos;
    Quaternion rot;
    
    public float inputView, inputView2;

    public float torque;

    public UIConsole console;

    void Start()
    {
        maneuverState = 0;
        maneuverIndex = 0;
    }

    void FixedUpdate()
    {
        if (Time.time > perSecondTime)
        {
            perSecondTime = Time.time + 1;
            PerSecond();
        }

        if (!vehicleIsOn)
        {
            if (!isStoped)
            {
                StopCar();
            }
        }
        else
        {
            if (ultrassonicSensor[0].value > 1 && !inManeuver)
            {
                if (ultrassonicSensor[1].value < 0.3f)
                {
                    AccelerateMotorReverse(0,1);
                    FreeBrake(3,2);
                    AccelerateMotor(3,2);
                }
                else if (ultrassonicSensor[2].value < 0.3f)
                {
                    AccelerateMotorReverse(3,2);
                    FreeBrake(0,1);
                    AccelerateMotor(0,1);
                }
                else if (RGBsensor[0].reflection == 0)
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
                if (!isStoped)
                {
                    StopCar();
                }
            }
        }
    }

    void PerSecond()
    {
        console.WriteMessage("Distance: " + ultrassonicSensor[0].value.ToString(), new TypeInfo());
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
            console.WriteMessage("Arrow " + dir.ToString(), new TypeInfo());
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
        isStoped = true;

        PlaySound(false);
        
        console.WriteMessage("Vehicle Stop!", new TypeWarning());

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
        isStoped = false;

        PlaySound(true);

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
        isStoped = false;

        PlaySound(true);

        SetSpeed(wheel1);

        if(speed > (topSpeed * (-1)))
        {
            colliders[wheel1].motorTorque = torque;
            colliders[wheel2].motorTorque = torque;
        }
        else
        {
            NoAccelerate(wheel1, wheel2);
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
            case 2:
                
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
            if (!forAuxTime)
            {
                upTime = Time.time + 1.5f;
                forAuxTime = true;
                GearUp(platformAnimator);
            }
            if (upTime < Time.time)
            {
                ManeuverStateIncrement();
                forAuxTime = false;
            }
        }
        else if (maneuverState == 1)
        {
            if (!forAuxTime)
            {
                turnTime = Time.time + 4.1f;
                forAuxTime = true;
            }

            if (turnTime > Time.time)
            {
                FreeBrakeAll();
                HalfTurn();
            }
            else 
            {
                if (inManeuver)
                {
                    StopCar();
                    inManeuver = false;
                }
            }
            
        }
    }

    void GearUp(Animator animator)
    {
        console.WriteMessage("Platform Up", new TypeInfo());
        animator.SetInteger("state", 1);
    }

    void HalfTurn()
    {
        AccelerateMotor(0,1);
        AccelerateMotorReverse(3,2);
    }

    void GearDown(Animator animator)
    {
        console.WriteMessage("Platform Down", new TypeInfo());
        animator.SetInteger("state", 2);
    }

    void PlaySound(bool play)
    {
        if (!isSoundPlay && play)
        {
            isSoundPlay = true;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if (isSoundPlay && !play)
        {
            isSoundPlay = false;
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }

    public void PublicStopCar()
    {
        StopCar();
    }
}

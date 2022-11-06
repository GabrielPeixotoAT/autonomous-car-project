using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Batery : MonoBehaviour
{
    public float charge, timeToDegrase;

    bool isRecharging, vehicleIsOn;

    void Start()
    {
        timeToDegrase = Time.time + 1.0f;
        //charge = 100;
        TurnOn();
    }

    void Update() 
    {
        if (charge <= 0)
        {
            TurnOff();
        }

        BateryUse(vehicleIsOn);
    }

    public void BateryUse(bool vehicleIsOn)
    {
        if (vehicleIsOn)
        {
            if (!isRecharging)
            {
                if (timeToDegrase < Time.time)
                {
                    timeToDegrase = Time.time + 0.1f;
                    charge -= 0.06f;
                }
            }
            else
            {
                if (timeToDegrase < Time.time)
                {
                    timeToDegrase = Time.time + 1.0f;
                    charge += 1;
                }
            }
        }
    }

    public void ChargerConected()
    {
        isRecharging = true;
    }

    public void ChargerDisconected()
    {
        isRecharging = false;
    }

    public void TurnOn()
    {
        vehicleIsOn = true;
        gameObject.GetComponent<CarController>().vehicleIsOn = true;
    }

    public void TurnOff()
    {
        vehicleIsOn = false;
        gameObject.GetComponent<CarController>().vehicleIsOn = false;
    }
}

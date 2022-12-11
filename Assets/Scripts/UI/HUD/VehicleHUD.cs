using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleHUD : MonoBehaviour
{
    public string Name;
    public float Batery;
    public Status Status_;

    public float[] Sensors;

    public Text TextName, TextBatery, TextStatus;

    void LateStart()
    {
        TextName.text = "Name: " + Name;
    }

    void Update()
    {
        UpdateTexts();
    }

    void UpdateTexts()
    {
        if (Batery < 10)
        {
            TextBatery.color = Color.red;
        } else if (Batery < 20)
        {
            TextBatery.color = Color.yellow;
        } else 
        {
            TextBatery.color = Color.green;
        }
        
        TextBatery.text = "Batery: " + Batery.ToString("F1") + "%";
        
        switch (Status_)
        {
            case Status.Runing: TextStatus.text = "Status: Runing"; break;
            case Status.Stoped: TextStatus.text = "Status: Stoped"; break;
        }
    }
}

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

    void Start()
    {
        TextName.text = "Name: " + Name;
    }

    void Update()
    {
        UpdateTexts();
    }

    void UpdateTexts()
    {
        TextBatery.text = "Batery: " + Batery.ToString("F1") + "%";
        
        switch (Status_)
        {
            case Status.Runing: TextStatus.text = "Runing"; break;
            case Status.Stoped: TextStatus.text = "Stoped"; break;
        }
        
    }
}

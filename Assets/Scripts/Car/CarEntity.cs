using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEntity : MonoBehaviour
{
    public string Name;
    public VehicleHUD VehicleHud;

    Batery BateryClass;
    
    void Start()
    {
        VehicleHud.Name = Name;
        BateryClass = gameObject.GetComponent<Batery>();
    }

    // Update is called once per frame
    void Update()
    {
        VehicleHud.Batery = BateryClass.charge;
    }
}

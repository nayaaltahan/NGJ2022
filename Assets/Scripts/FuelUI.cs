using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    public Slider fuelBar;
    public PlayerFuelController fuelController;
 
    void Update()
    {
        fuelBar.value = fuelController.currentFuel;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    public Image fuelBar;
    public PlayerFuelController fuelController;
 
    void Update()
    {
        fuelBar.fillAmount = fuelController.currentFuel / fuelController.fullFuel;
    }
    
}

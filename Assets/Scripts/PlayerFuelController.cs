using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuelController : MonoBehaviour
{
    public float fullFuel = 100f;
    [HideInInspector]
    public float currentFuel;
    public float fuelPerSecond = 5f;
    public float fuelEarned = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        currentFuel = fullFuel;
        StartCoroutine(SpendFuel());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFuel <= 0)
        {
            // unity log message: "Fuel is empty" 
            Debug.Log("Fuel is empty");
            StopCoroutine(SpendFuel());
        }
    }
    
    IEnumerator SpendFuel()
    {
        while (currentFuel > 0)
        {
            currentFuel -= fuelPerSecond * Time.deltaTime;
            yield return null;
        }
    }
    
    public void Refuel()
    {
        currentFuel += fuelEarned;
        if (currentFuel > fullFuel)
        {
            currentFuel = fullFuel;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 110, 100, 20), "Fuel: " + currentFuel);
    }
}

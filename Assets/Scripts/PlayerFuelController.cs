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

    public GameObject fuelParticles;

    private float fuelParticleAliveTime = 0;

    public static PlayerFuelController Instance;

    private bool died = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("TWO PLAYER FUELS ACTIVE");
            Destroy(gameObject);
        }
        currentFuel = fullFuel;
        StartCoroutine(SpendFuel());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFuel <= 0 && !died)
        {
            died = true;
            // unity log message: "Fuel is empty" 
            Debug.Log("Fuel is empty");
            StopCoroutine(SpendFuel());
            LevelManager.Instance.KillPlayer();
        }

        if (fuelParticles.activeSelf)
        {
            fuelParticleAliveTime += Time.deltaTime;
        }

        if (fuelParticleAliveTime >= 2)
        {
            fuelParticles.SetActive(false);
            fuelParticleAliveTime = 0.0f;
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


}

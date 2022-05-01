using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FuelBrick : MonoBehaviour
{
    private Vector3 rotationDirection;
    public PlayerFuelController playerFuelController;

    // Start is called before the first frame update
    void Start()
    {
        // Set a random rotation
        rotationDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        playerFuelController = GameObject.FindWithTag("Player").GetComponent<PlayerFuelController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate in a random direction
        transform.Rotate(rotationDirection * 0.4f * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayOneShot("event:/Effects/Fuel_PickUp");
            playerFuelController.fuelParticles.SetActive(true);
            playerFuelController.Refuel();
            Destroy(gameObject);
        }
    }
}

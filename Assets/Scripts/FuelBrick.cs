using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FuelBrick : MonoBehaviour
{
    private Vector3 rotationDirection;


    // Start is called before the first frame update
    void OnEnable()
    {
        // Set a random rotation
        rotationDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
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
            PlayerFuelController.Instance.fuelParticles.SetActive(true);
            PlayerFuelController.Instance.Refuel();
            Destroy(gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private GameObject explosion;
    private Vector3 rotationDirection;

    private bool isExploding = false;
    // Start is called before the first frame update
    void Start()
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

    public void DestroyAsteroid()
    {
        if (!isExploding)
        {
            isExploding = true;
            // Instantiate the explosion
            var expl = Instantiate(explosion, transform.position, Quaternion.identity);
            expl.SetActive(true);
            Destroy(expl, 5f);
            // Destroy the asteroid
            Destroy(gameObject);
        }
    }
}

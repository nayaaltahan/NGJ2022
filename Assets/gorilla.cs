using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gorilla : MonoBehaviour
{
    private Vector3 rotationDirection;

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
    
}

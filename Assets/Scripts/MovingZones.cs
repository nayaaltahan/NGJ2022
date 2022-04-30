using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingZones : MonoBehaviour
{
    public float speed = 4; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position.WithZ(transform.position.z - speed * Time.fixedDeltaTime);
    }
}

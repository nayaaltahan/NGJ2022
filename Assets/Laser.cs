using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 100f;

    private Vector3 direction = Vector3.zero;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 4.0f);
    }

    private void FixedUpdate()
    {
        if(direction != Vector3.zero)
            rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
    }

    public void Shoot(Vector3 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Laser hit " + other.name);
        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Laser hit Asteroidddd");
            var asteroid = other.GetComponent<Asteroid>();
            asteroid.DestroyAsteroid();

        }
    }
}

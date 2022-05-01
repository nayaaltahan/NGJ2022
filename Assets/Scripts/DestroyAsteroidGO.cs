using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroidGO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndOfLevel"))
        {
            Debug.Log("End of level");;
            LevelManager.Instance.DequeueLevel();
        }
    }
}

using System;
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

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var child in gameObject.GetComponentsInChildren<Transform>(true))
        {
            var tag = child?.tag;
            if (tag != "Gorilla" && tag != "Asteroid" && tag != "FuelBrick" && tag != "EndOfLevel")
                continue;
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position.WithZ(transform.position.z - speed * Time.fixedDeltaTime);
    }
}

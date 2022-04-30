using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaser : MonoBehaviour
{

    [SerializeField] private Transform shootFrom1, shootFrom2;
    
    [SerializeField] private GameObject laserPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var laser = Instantiate(laserPrefab, shootFrom1.position, Quaternion.identity);
        var laser2 = Instantiate(laserPrefab, shootFrom2.position, Quaternion.identity);
        
        laser.SetActive(true);
        laser2.SetActive(true);
        
        laser.GetComponent<Laser>().Shoot(transform.forward);
        laser2.GetComponent<Laser>().Shoot(transform.forward);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaser : MonoBehaviour
{

    [SerializeField] private Transform shootFrom1, shootFrom2;
    
    [SerializeField] private GameObject laserPrefab;

    [SerializeField] private ForceSensor forceSensor;

    [SerializeField] private float shootDelay = 0.3f;
    
    private float timeSinceLastShot = 0;
    
    private PlayerController playerController;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private bool shouldShoot = false;
    
    // Update is called once per frame
    void Update()
    {
        if (playerController.controlScheme == ControlScheme.KEYBOARD)
        {
            shouldShoot = Input.GetButtonDown("Jump");
        }
        else if (forceSensor.IsConnected)
            shouldShoot = forceSensor.Force > 0.4f;
        if(shouldShoot && timeSinceLastShot > shootDelay)
        {
            timeSinceLastShot = 0;
            Shoot();
        }
        else
            timeSinceLastShot += Time.deltaTime;
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

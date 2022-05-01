using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killplayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.KillPlayer();
        }
    }
}

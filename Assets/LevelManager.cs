using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] easyLevels;
    [SerializeField] private GameObject[] mediumLevels;
    [SerializeField] private Transform levelSpawnPoint;

    public float currentScore;
    [SerializeField] private Queue<GameObject> currentLevels;

    [SerializeField] private float currentSpeed = 15;

    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject explosion;
    public static LevelManager Instance { get; private set; }

    public GameState currentState { get; private set; } = GameState.Playing;
    

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            currentLevels = new Queue<GameObject>();
            EnqueueLevel(easyLevels[UnityEngine.Random.Range(0, easyLevels.Length)]);
            StartCoroutine(LevelCoroutine());
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (currentState == GameState.Playing)
            currentScore += Time.deltaTime * 10;
    }

    private IEnumerator LevelCoroutine()
    {
        yield return new WaitForSeconds(11);
        EnqueueLevel(easyLevels[UnityEngine.Random.Range(0, easyLevels.Length)]);

    }

    
    public void DequeueLevel()
    {
        Debug.Log("Dequeuing level");
        if (currentLevels.Count > 0)
        {
            var front = currentLevels.Dequeue();
            // if score is above certian point, likelyhood to spawn a medium level is higher
            if (currentScore > 10)
            {
                var random = UnityEngine.Random.Range(0, 100);
                if (random < 50)
                {
                    EnqueueLevel(mediumLevels[UnityEngine.Random.Range(0, mediumLevels.Length)]);
                }
                else
                {
                    EnqueueLevel(easyLevels[UnityEngine.Random.Range(0, easyLevels.Length)]);
                }
            }
            else
            {
                EnqueueLevel(easyLevels[UnityEngine.Random.Range(0, easyLevels.Length)]);
            }
            Debug.Log("Front: " + front.name, front);
            front.SetActive(false);
            if(currentSpeed < 100)
                currentSpeed += 5;
        }
    }

    private void EnqueueLevel(GameObject level)
    {
        var levelOb = Instantiate(level, levelSpawnPoint.position, Quaternion.identity);
        levelOb.GetComponent<MovingZones>().speed = currentSpeed;
        currentLevels.Enqueue(levelOb);
    }

    public void KillPlayer()
    {
        var expl = Instantiate(explosion, playerModel.transform.position, Quaternion.identity);
        expl.SetActive(true);
        playerModel.SetActive(false);
        currentState = GameState.Paused;
    }
}

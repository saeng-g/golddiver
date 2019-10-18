using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public Vector2 startingPos;
    public GameObject goldSmallPrefab;
    public GameObject goldMedPrefab;
    public GameObject goldLargePrefab;
    public List<GameObject> spawnedGold;
    public Rigidbody2D player;
    public const int maxNbGold = 5;
    public bool gameStarted = false;

    public float spawnTime = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        spawnedGold = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            CountTime();
            if (spawnTime <= 0.0f)
            {
                spawnedGold.RemoveAll(x => x == null);
                if (maxNbGold > spawnedGold.Count)
                {
                    Spawn();
                }
                spawnTime = 3.0f;
            }
        }
    }

    void Spawn()
    {
        // randomly decide which gold to spawn
        float goldGen = Random.Range(0.0f, 10.0f);
        if (goldGen < 6.0f)
        {
            // 60% chance of small
            startingPos = new Vector2(Random.Range(-3.5f, 3.5f), -2.16f);
            GameObject gold = Instantiate(goldSmallPrefab, startingPos, Quaternion.identity) as GameObject;
            gold.GetComponent<Gold>().SetSizeToSmall();
            spawnedGold.Add(gold);
        }
        else if (goldGen < 9.0f)
        {
            // 30% chance of med
            startingPos = new Vector2(Random.Range(-3.5f, 3.5f), -2.087f);
            GameObject gold = Instantiate(goldMedPrefab, startingPos, Quaternion.identity) as GameObject;
            gold.GetComponent<Gold>().SetSizeToMed();
            spawnedGold.Add(gold);
        }
        else
        {
            // 10% chance of large
            startingPos = new Vector2(Random.Range(-3.5f, 3.5f), -2.087f);
            GameObject gold = Instantiate(goldLargePrefab, startingPos, Quaternion.identity) as GameObject;
            gold.GetComponent<Gold>().SetSizeToLarge();
            spawnedGold.Add(gold);
        }
    }

    void CountTime()
    {
        if (gameStarted)
        {
            spawnTime -= Time.deltaTime;
        }
    }

    //Methods to Signal that game has started/Ended
    public void StartGame()
    {
        gameStarted = true;
        spawnTime = 3.0f;
    }

    public void EndGame()
    {
        gameStarted = false;
        for (int i = 0; i < spawnedGold.Count; i++)
        {
            Destroy(spawnedGold[i]);
        }
        spawnedGold.Clear();
    }
}

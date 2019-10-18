using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public Vector2 startingPos;
    public GameObject sharkPrefab;
    public List<GameObject> spawnedSharks;
    public Rigidbody2D player;
    public const int maxNbSharks = 10;
    public bool gameStarted = false;
    public int currentLevel = 1;

    public float spawnTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        spawnedSharks = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            CountTime();
            if (spawnTime <= 0.0f)
            {
                spawnedSharks.RemoveAll(x => x == null);
                if (maxNbSharks > spawnedSharks.Count)
                {
                    Spawn();
                }
                spawnTime = 3.0f;
            }
        }
    }

    void Spawn()
    {
        // get starting position that is acceptably distant from the current player
        // to avoid the critter spawning on top of the position.
        startingPos = new Vector3(
            Random.Range(-3.5f, 3.5f), Random.Range(-1.7f, 0.75f));
        while (Mathf.Abs(startingPos.x - player.position.x) < 2f)
        {
            startingPos = new Vector3(
                Random.Range(-3.5f, 3.5f), Random.Range(-1.7f, 0.75f));
        }
        // Spawn shark and add the spawned instance to spawnedSharks List
        if (startingPos.x <= 0)
        {
            GameObject shark = Instantiate(sharkPrefab, startingPos, Quaternion.AngleAxis(180, new Vector3(0, 1, 0))) as GameObject;
            float sizeCoeff = (int) Random.Range(1.0f, 3.0f);
            shark.transform.localScale = new Vector2(sizeCoeff*0.3f, sizeCoeff*0.3f);
            shark.GetComponent<Shark>().ChangeVelocity(currentLevel);
            spawnedSharks.Add(shark);
        }
        else
        {
            GameObject shark = Instantiate(sharkPrefab, startingPos, Quaternion.identity) as GameObject;
            float sizeCoeff = (int)Random.Range(1.0f, 3.0f);
            shark.transform.localScale = new Vector2(sizeCoeff * 0.3f, sizeCoeff * 0.3f);
            shark.GetComponent<Shark>().ChangeVelocity(currentLevel);
            spawnedSharks.Add(shark);
        }
    }

    void CountTime()
    {
        if (gameStarted)
        {
            spawnTime -= Time.deltaTime;
        }
    }

    public void LevelUp(int level)
    {
        currentLevel = level;
    }

    //Methods to Signal that game has started/Ended
    public void StartGame()
    {
        gameStarted = true;
        currentLevel = 1;
        spawnTime = 3.0f;
    }
    public void EndGame()
    {
        gameStarted = false;
        for (int i = 0; i < spawnedSharks.Count; i++)
        {
            Destroy(spawnedSharks[i]);
        }
        spawnedSharks.Clear();
    }
}

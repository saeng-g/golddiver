using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusSpawner : MonoBehaviour
{
    public Vector2 startingPos;
    public GameObject octopusPrefab;
    public List<GameObject> spawnedOctopus;
    public Rigidbody2D player;
    public const int maxNbOctopus = 2;
    public bool gameStarted = false;

    public float spawnTime = 5.0f;
    public int currentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        spawnedOctopus = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            CountTime();
            if (spawnTime <= 0.0f)
            {
                spawnedOctopus.RemoveAll(x => x == null);
                if (maxNbOctopus > spawnedOctopus.Count)
                {
                    Spawn();
                }
                spawnTime = 7.0f;
            }
        }
    }

    void Spawn()
    {
        // get starting position that is acceptably distant from the current player
        // to avoid the critter spawning on top of the position.
        startingPos = new Vector3(
            Random.Range(-3.5f, 3.5f), Random.Range(-1.7f, 0.75f));
        while (Mathf.Abs(startingPos.x - player.position.x) < 1f)
        {
            startingPos = new Vector3(
                Random.Range(-3.5f, 3.5f), Random.Range(-1.7f, 0.75f));
        }
        // Spawn shark and add the spawned instance to spawnedSharks List
        if (startingPos.x <= 0)
        {
            GameObject octopus = Instantiate(octopusPrefab, startingPos, Quaternion.AngleAxis(180, new Vector3(0, 1, 0))) as GameObject;
            float sizeCoeff = (int)Random.Range(1.0f, 3.0f);
            octopus.transform.localScale = new Vector2(sizeCoeff * 0.3f, sizeCoeff * 0.3f);
            octopus.GetComponent<Octopus>().ChangeVelocity(currentLevel);
            spawnedOctopus.Add(octopus);
        }
        else
        {
            GameObject octopus = Instantiate(octopusPrefab, startingPos, Quaternion.identity) as GameObject;
            float sizeCoeff = (int)Random.Range(1.0f, 3.0f);
            octopus.transform.localScale = new Vector2(sizeCoeff * 0.3f, sizeCoeff * 0.3f);
            octopus.GetComponent<Octopus>().ChangeVelocity(currentLevel);
            spawnedOctopus.Add(octopus);
        }
    }

    void CountTime()
    {
        if (gameStarted)
        {
            spawnTime -= Time.deltaTime;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        currentLevel = 1;
        spawnTime = 5.0f;
    }

    public void EndGame()
    {
        gameStarted = false;
        for (int i = 0; i < spawnedOctopus.Count; i++)
        {
            Destroy(spawnedOctopus[i]);
        }
        spawnedOctopus.Clear();
    }

    public void LevelUp(int level)
    {
        currentLevel = level;
    }
}

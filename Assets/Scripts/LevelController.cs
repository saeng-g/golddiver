using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// keeps track of levels and sends commands to other relavant 
public class LevelController : MonoBehaviour
{
    public int currentLevel = 1;
    public int score = 0;

    public float levelStartDelay = 2.0f;
    public float levelTime = 2.0f;
    public Text levelText;
    public Text scoreText;
    public bool gameInProgress = false;

    public Ship ship;
    public OctopusSpawner oSpawner;
    public SharkSpawner sSpawner;
    public GoldSpawner gSpawner;
    public PlayerMvt diver;

    public GameObject gameOverMenu;

    // Update is called once per frame
    void Update()
    {
        if (gameInProgress)
        {
            score = ship.score;
            levelText.text = "Level: " + currentLevel;
            scoreText.text = "Score: " + score;
            if (levelTime < 0.0f)
            {
                LevelUp();
            }
            if (diver.life == 0)
            {
                EndGame();
            }
            CountTime();
        }
    }

    public void InitiateLevel(bool normalMode)
    {
        gameInProgress = true;
        currentLevel = 1;
        score = 0;
        ship.score = 0;
        diver.StartGame(!normalMode);
        oSpawner.StartGame();
        sSpawner.StartGame();
        gSpawner.StartGame();

    }

    public void EndGame()
    {
        //reset
        gameInProgress = false;
        oSpawner.LevelUp(1);
        sSpawner.LevelUp(1);
        //end game
        oSpawner.EndGame();
        sSpawner.EndGame();
        gSpawner.EndGame();
        //game over msg
        gameOverMenu.SetActive(true);
    }

    void CountTime()
    {
        levelTime -= Time.deltaTime;
    }

    private void LevelUp()
    {
        currentLevel += 1;
        levelTime = 20.0f;
        oSpawner.LevelUp(currentLevel);
        sSpawner.LevelUp(currentLevel);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    public LevelController lvlController;
    public GameObject startMenu;

    public void StartGame(bool normalGame)
    {
        lvlController.InitiateLevel(normalGame);
        startMenu.SetActive(false);
    }

    public void EndGame()
    {
        startMenu.SetActive(true);
    }
}

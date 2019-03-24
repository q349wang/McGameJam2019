using MapGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // these values must only be messed with on the server
    public static int currentSurvivors = -1;
    public static int currentLegends = -1;

    NetworkedGameManager manager;

    void Start()
    {
        manager = GetComponent<NetworkedGameManager>();
    }

    void Update()
    {
        if (currentSurvivors == 0 || currentLegends == 0)
        {
            DoGameOver();
        }
    }

    // only called on the server
    void DoGameOver()
    {
        Debug.Log("Game Over");
        currentSurvivors = -1;
        currentLegends = -1;
        if (manager)
        {
            manager.ServerResetGame();
        }
    }

    

    public static void AddSurvivor()
    {
        if (currentSurvivors == -1) currentSurvivors++;
        currentSurvivors++;
    }
    public static void AddLegend()
    {
        if (currentLegends == -1) currentLegends++;
        currentLegends++;
        Debug.Log("Added 1 legend");
    }

    public static void SurvivorDied()
    {
        currentSurvivors--;
        Debug.Log(currentSurvivors + " survivors left.");
    }
    public static void LegendDied()
    {
        currentLegends--;
        Debug.Log(currentLegends + " legends left.");
    }
}

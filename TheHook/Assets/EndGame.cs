using MapGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // these values must only be messed with on the server
    public static int currentSurvivors = -1;
    public static int currentLegends = -1;

    GenerateMap mapGen = null;

    // Start is called before the first frame update
    void Start()
    {
        mapGen = GetComponent<GenerateMap>();
        if (mapGen == null) Debug.LogError("EndGame: No map generator found.");
        //currentLegends = FindObjectsOfType<Hooker>().Length;
        //currentSurvivors = FindObjectsOfType<BasePlayer>().Length - currentLegends;

        //Debug.Log(currentSurvivors);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSurvivors == 0 || currentLegends == 0)
        {
            DoGameOver();
        }
    }

    void DoGameOver()
    {
        Debug.Log("Game Over");
        Invoke("RegenerateMap", 3f);
    }

    void RegenerateMap()
    {
        mapGen.Regenerate(0);
    }

    public static void AddSurvivor()
    {
        if (currentSurvivors == -1) currentSurvivors++;
        currentSurvivors++;
    }
    public static void AddHooker()
    {
        if (currentLegends== -1) currentLegends++;
        currentLegends++;
    }
}

using MapGen;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkedGameManager : NetworkBehaviour
{
    GenerateMap mapGen = null;
    [SyncVar(hook = "OnLegendScoreChanged")]
    int legendScore = 0;
    [SyncVar(hook = "OnSurvivorScoreChanged")]
    int survivorScore = 0;

    TextMeshProUGUI legendScoreText;
    TextMeshProUGUI survivorScoreText;

    void Start()
    {
        mapGen = FindObjectOfType<GenerateMap>();
        if (mapGen == null) Debug.LogError("Networked Manager: No map generator found.");

        legendScoreText = GameObject.Find("LegendScore").GetComponent<TextMeshProUGUI>();
        survivorScoreText = GameObject.Find("SurvivorScore").GetComponent<TextMeshProUGUI>();
        if (legendScoreText == null || survivorScoreText == null) Debug.LogError("Text object not found");
    }

    ///// Restarting Game /////
    public void ServerResetGame()
    {
        if (isServer)
        {
            RpcResetGame();
        }
    }

    [ClientRpc]
    void RpcResetGame()
    {
        Debug.Log("Restarting...");
        
        Invoke("RegenerateMap", 3f);
    }

    void RegenerateMap()
    {
        foreach (BasePlayer player in FindObjectsOfType<BasePlayer>())
        {
            Debug.Log("Unkilling player");
            player.ResetPlayer();
        }
        mapGen.Regenerate();
    }

    ///// Scoring /////

    public void ServerLegendScored()
    {
        if (isServer) legendScore++;
    }
    public void ServerSurvivorScored()
    {
        if (isServer) survivorScore++;
    }

    void OnLegendScoreChanged(int newScore)
    {
        legendScoreText.text = newScore.ToString();
    }

    void OnSurvivorScoreChanged(int newScore)
    {
        survivorScoreText.text = newScore.ToString();
    }

}

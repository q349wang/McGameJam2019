using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedGameManager : NetworkBehaviour
{
    GenerateMap mapGen = null;

    void Start()
    {
        mapGen = FindObjectOfType<GenerateMap>();
        if (mapGen == null) Debug.LogError("Networked Manager: No map generator found.");
    }

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


[ClientRpc]
    void RpcRegenerateMap()
    {
        foreach (BasePlayer player in FindObjectsOfType<BasePlayer>())
        {
            Debug.Log("Unkilling player");
            player.ResetPlayer();
        }
        System.Random rng = new System.Random();
        mapGen.RegenerateMap(rng.Next());
    }

}

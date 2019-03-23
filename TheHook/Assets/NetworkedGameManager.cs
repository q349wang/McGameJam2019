using MapGen;
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

    //[Command]
    public void CmdResetGame()
    {
        RpcResetGame();
    }

    [ClientRpc]
    void RpcResetGame()
    {
        Debug.Log("Restarting...");
        
        Invoke("RegenerateMap", 3f);
    }

    void RegenerateMap()
    {
        foreach (PlatformerCharacter2D player in FindObjectsOfType<PlatformerCharacter2D>())
        {
            Debug.Log("Unkilling player");
            player.Kill(false);
        }
        mapGen.Regenerate();
    }

}

using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TypedPlayerSpawner : NetworkBehaviour
{
    
    [SyncVar]
    public int classIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (classIndex == 0)
        {
            gameObject.AddComponent<Healer>();
        }
        else if (classIndex == 1)
        {
            gameObject.AddComponent<Tank>();
        }
        GetComponent<SpriteRenderer>().sprite = LobbyPlayer.Sprites[classIndex];
    }

}

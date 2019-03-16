using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //base.OnServerAddPlayer(conn, playerControllerId);
        GameObject player = Instantiate(spawnPrefabs[conn.connectionId % spawnPrefabs.Count], GetStartPosition().position, Quaternion.identity);
        player.name += conn.connectionId.ToString();
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

}

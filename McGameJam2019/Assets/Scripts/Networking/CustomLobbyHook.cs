using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

enum CharacterClass
{
    Legend = 0,

}

public class CustomLobbyHook : Prototype.NetworkLobby.LobbyHook
{
    [SerializeField]
    int prefabIndex = 0;

    [SerializeField]
    GameObject[] playerPrefabs;

    void OnValidate()
    {
        //GetComponent<NetworkLobbyManager>().gamePlayerPrefab = playerPrefabs[prefabIndex];
    }

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        //gamePlayer = Instantiate(playerPrefab, gamePlayer.transform.position, Quaternion.identity);
        //NetworkServer.Spawn(playerPrefab);
    }
}

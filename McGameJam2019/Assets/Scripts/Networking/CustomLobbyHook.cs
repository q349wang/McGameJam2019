using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

enum CharacterClass
{
    Legend = 0,

}

public class CustomLobbyHook : LobbyHook
{
    void OnValidate()
    {
        //GetComponent<NetworkLobbyManager>().gamePlayerPrefab = playerPrefabs[prefabIndex];
    }

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        Debug.Log("Setting sprite: " + lobby.playerClassSprite);
        gamePlayer.GetComponent<TypedPlayerSpawner>().classIndex = lobby.playerClassSprite;
    }
}

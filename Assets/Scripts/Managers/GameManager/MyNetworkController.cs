using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkController : NetworkManager
{
    private bool playerOneConnected = false;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log(playerControllerId);
        // Spawn Boy prefab if it is the player 1.
        if (!playerOneConnected)
        {
            var player = (GameObject) GameObject.Instantiate(
                spawnPrefabs[0], 
                new Vector3(0,0,0), 
                Quaternion.identity
            );
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            playerOneConnected = true;
        }

        // Spawn the Light prefab if it is the player 2.
        else
        {
            var player = (GameObject)GameObject.Instantiate(
                spawnPrefabs[1],
                new Vector3(0, 20f, 0),
                Quaternion.identity
            );
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        base.OnServerAddPlayer(conn, playerControllerId);
    }
}


using PurrNet;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private KeyCode spawnKey;
    [SerializeField] private NetworkIdentity characterPrefab;
    [SerializeField] private Transform container;

    [SerializeField] List<PlayerID> connections = new();
    private PlayerID myPlayerID;
    private void Awake()
    {
        NetworkManager.main.onPlayerJoined += onPlayerJoined;
        NetworkManager.main.onLocalPlayerReceivedID += Main_onLocalPlayerReceivedID;
        
    }

    private void Main_onLocalPlayerReceivedID(PlayerID player)
    {
        myPlayerID = player;
        print("получил свой id " + player.id);

        var c = Instantiate(characterPrefab, container.position, Quaternion.identity);
        c.GiveOwnership(player);
    }

    private void onPlayerJoined(PlayerID player, bool isReconnect, bool asServer)
    {
        if (asServer)
        {
            //connections.Add(player);
            return;
        }
        
        
    }

    private void NetworkManager_onPlayerJoined(PlayerID player, bool isReconnect, bool asServer)
    {
        print(player);
    }

    private void Update()
    {
        //if (!NetworkManager.main.isHost) { return; }
        //if (Input.GetKeyDown(spawnKey))
        //{
        //    connections.ForEach(connection =>
        //    {
        //        var c = Instantiate(characterPrefab, parentPrefab.position, Quaternion.identity);
        //        c.GiveOwnership(connection);
        //    });
        //}
        
        //if (Input.GetKeyDown(spawnKey))
        //{
        //    var c = Instantiate(characterPrefab, parentPrefab.position, Quaternion.identity);
        //    c.GiveOwnership(NetworkManager.main.localPlayer);
        //}
    }
}

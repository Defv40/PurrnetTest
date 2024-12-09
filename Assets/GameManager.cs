using PurrNet;
using UnityEngine;
using PurrNet.Transports;
public class GameManager : MonoBehaviour
{
    [SerializeField] private KeyCode hostKey;
    [SerializeField] private KeyCode clientKey;
    [SerializeField] private KeyCode spawnKey;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Transform parentPrefab;
    // Update is called once per frame
    private void Awake()
    {
        NetworkManager.main.onClientConnectionState += onClientConnectionState;
        NetworkManager.main.onLocalPlayerReceivedID += onLocalPlayerReceivedID;
        NetworkManager.main.onServerConnectionState += onServerConnectionState;
        NetworkManager.main.onPlayerJoined += onPlayerJoined;
        NetworkManager.main.onPlayerJoinedScene += onPlayerJoinedScene;
        NetworkManager.main.onPlayerLeft += onPlayerLeft;
        NetworkManager.main.onPlayerLeftScene += onPlayerLeftScene;
        NetworkManager.main.onPlayerLoadedScene += onPlayerLoadedScene;
    }

    private void onPlayerLoadedScene(PlayerID player, SceneID scene, bool asserver)
    {
        print("Load Scene");
    }

    private void onPlayerLeftScene(PlayerID player, SceneID scene, bool asserver)
    {
        throw new System.NotImplementedException();
    }

    private void onPlayerLeft(PlayerID player, bool asServer)
    {
        print("onPlayerLeft");
    }

    private void onPlayerJoinedScene(PlayerID player, SceneID scene, bool asserver)
    {
        print("SceneID " + scene.id + " " + asserver);
    }

    private void onPlayerJoined(PlayerID player, bool isReconnect, bool asServer)
    {
        print("asServer " + asServer);
        print("onPlayerJoined");
    }

    private void onServerConnectionState(ConnectionState obj)
    {
        print("server " + obj.ToString());
    }

    private void onLocalPlayerReceivedID(PlayerID player)
    {
        print(player.ToString());   
    }

    private void onClientConnectionState(ConnectionState obj)
    {
        print("client"+ " " + obj.ToString());
    }

    private void Update()
    {
        if (NetworkManager.main.isOffline)
        {
            if (Input.GetKeyDown(hostKey))
            {
                NetworkManager.main.StartServer();
                NetworkManager.main.StartClient();

            }

            if (Input.GetKeyDown(clientKey))
            {
                NetworkManager.main.StartClient();
            }
        }
        else
        {
            

           
           
        }

    }

    
}

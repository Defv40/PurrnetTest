using PurrNet;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private Button StartLobbyBtn;
    [SerializeField] private Button JoinLobbyBtn;
    [SerializeField] private Button ExitBtn;
    [SerializeField] private CinemachineCamera tableCamera;
    [SerializeField] private CinemachineCamera lobbyCamera;
    [SerializeField] private GameObject StartMenu;

    [SerializeField] private Button LeaveLobby;
    [SerializeField] private Button StartGame;
    [SerializeField] private NetworkManager _networkManager;
    [SerializeField] private Button nextCharacterBtn;
    [SerializeField] private Button previousCharacterBtn;

    [SerializeField] private List<GameObject> lobbyCharactersPrefabs = new List<GameObject>();
    private int currentIdCharacter = 1;
    private GameObject currentCharacter;
    private LobbyCharacterSpawner spawner;


    [SerializeField] private Transform spawnPoints;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    private Transform currentPoint;
    private Dictionary<PlayerID, int> playersPositionsInLobby = new Dictionary<PlayerID, int>();
    private void HideTableMenu()
    {
        tableCamera.Priority = 0;
        lobbyCamera.Priority = 999;
        StartMenu.SetActive(false);
    }

    protected override void OnSpawned(bool asServer)
    {
        if (asServer)
        {
            networkManager.onPlayerLeft += NetworkManager_onPlayerLeft;
            return;
        }
        
        //if (_networkManager.isServer)
        //{
        //    currentPoint = spawnPoints.GetChild(0).transform;
        //}
        //else
        //{
        //    Server_SetupClientPosition();
        //}

        Server_SetupClientPosition();
        //currentCharacter = spawner.SpawnCharacter(lobbyCharactersPrefabs[currentIdCharacter], currentPoint);
        tableCamera.Prioritize();
    }

    private void ShowTableMenu()
    {
        tableCamera.Priority = 999;
        lobbyCamera.Priority = 0;
        StartMenu.SetActive(true);
    }

    private void Awake()
    {
        spawner = GetComponent<LobbyCharacterSpawner>();
        StartLobbyBtn.onClick.AddListener(() =>
        {
            HideTableMenu();
            _networkManager.StartServer();
            _networkManager.StartClient();
        });



        ExitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        LeaveLobby.onClick.AddListener(() =>
        {
            ShowTableMenu();
            if (_networkManager.isServer)
            {
                _networkManager.StopServer();
            }

            _networkManager.StopClient();

        });

        StartGame.onClick.AddListener(() =>
        {

        });

        JoinLobbyBtn.onClick.AddListener(() =>
        {
            HideTableMenu();
            _networkManager.StartClient();
        });

        nextCharacterBtn.onClick.AddListener(() =>
        {
            currentIdCharacter++;
            if (currentCharacter != null)
                spawner.DespawnCharacter(currentCharacter);
            currentCharacter = spawner.SpawnCharacter(lobbyCharactersPrefabs[currentIdCharacter], currentPoint);
            nextCharacterBtn.interactable = currentIdCharacter < lobbyCharactersPrefabs.Count - 1;
            previousCharacterBtn.interactable = true;
        });

        

        previousCharacterBtn.onClick.AddListener(() =>
        {
            currentIdCharacter--;
            if (currentCharacter != null)
                spawner.DespawnCharacter(currentCharacter);
            currentCharacter = spawner.SpawnCharacter(lobbyCharactersPrefabs[currentIdCharacter], currentPoint);
            previousCharacterBtn.interactable = currentIdCharacter > 0;
            nextCharacterBtn.interactable = true;
        });

        //_networkManager.onLocalPlayerReceivedID += NetworkManager_onLocalPlayerReceivedID;

      
    }

    private void NetworkManager_onPlayerLeft(PlayerID player, bool asServer)
    {
        if (asServer)
        {
            playersPositionsInLobby.Clear();
        }
        Transform point = spawnPoints.GetChild(playersPositionsInLobby[player]);
        playersPositionsInLobby.Remove(player);
        Destroy(point.GetChild(0).gameObject);
    }

    [ServerRpc(requireOwnership: false, runLocally: false)]
    private void Server_SetupClientPosition(RPCInfo info = default)
    {
        //print(info.sender.id);

        for (int i = 0; i < spawnPoints.childCount; i++)
        {
            Transform point = spawnPoints.GetChild(i);
            if (point.childCount > 0) continue;
            else
            {
                var charac = spawner.SpawnCharacter(lobbyCharactersPrefabs[currentIdCharacter], point);
                NetworkID guid = charac.GetComponent<PrefabLink>().idServer.Value;
            
                Target_RecieveIndexPositionFromServer(info.sender, i, guid);
                playersPositionsInLobby.Add(info.sender, i);
                break;
            }
        }
    }

    [TargetRpc(requireServer: true, runLocally: false)]
    private void Target_RecieveIndexPositionFromServer(PlayerID target, int indexPosition, NetworkID guid)
    {
        print(indexPosition.ToString() + " Позиция");
        print(guid);
        currentPoint = spawnPoints.GetChild(indexPosition);



        currentCharacter = currentPoint.GetChild(0).gameObject;
        
    }

    private void NetworkManager_onLocalPlayerReceivedID(PlayerID player)
    {
        
    }

    private void Start()
    {
        previousCharacterBtn.interactable = false;
    }
}

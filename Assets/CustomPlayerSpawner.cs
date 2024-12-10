using PurrNet;
using UnityEngine;

public class CustomPlayerSpawner : MonoBehaviour
{
    public NetworkIdentity characterPrefab;
    public Transform spawnPoint;
    private void Awake()
    {
        NetworkManager.main.onLocalPlayerReceivedID += onLocalPlayerReceivedID;
    }

    private void onLocalPlayerReceivedID(PlayerID player)
    {
        var instance = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
        instance.GiveOwnership(player);
    }
}

using PurrNet;
using UnityEngine;
using PurrNet.Transports;
public class GameManager : MonoBehaviour
{
    [SerializeField] private KeyCode hostKey;
    [SerializeField] private KeyCode clientKey;
    

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
    }
}

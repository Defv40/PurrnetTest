using PurrNet;
using UnityEngine;

public class Connection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            NetworkManager.main.StartServer();
            NetworkManager.main.StartClient();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            NetworkManager.main.StartClient();
        }
    }
}

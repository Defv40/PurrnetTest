using NUnit.Framework;
using PurrNet;

using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject prefabItem;
    private Transform PlayerSpawner; // container for cubes 
    
    protected override void OnSpawned()
    {
        base.OnSpawned();
        enabled = isOwner;
        
    }
    private void Start()
    {
        PlayerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }
    private void Update()
    {
        // the client does not see when the server spawns
        if (Input.GetKeyDown(KeyCode.C))
        {
            var item = Instantiate(prefabItem, transform.position, Quaternion.identity, PlayerSpawner);
        }

        // It works, but this creates objects on the scene and not in the object

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    var item = Instantiate(prefabItem, transform.position, Quaternion.identity);
        //}
    }
}

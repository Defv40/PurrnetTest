
using PurrNet;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    public GameObject cubePrefab;
    private GameObject _currentCube;


    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
       
        enabled = isOwner;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentCube)
                Destroy(_currentCube);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentCube)
                Destroy(_currentCube);

            _currentCube = Instantiate(cubePrefab, transform.position + transform.forward, transform.rotation);
        }
          
    }
}

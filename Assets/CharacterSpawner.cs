using PurrNet;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private KeyCode spawnKey;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Transform parentPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            Instantiate(spawnPrefab, parentPrefab.position, Quaternion.identity, parentPrefab);
        }
    }
}

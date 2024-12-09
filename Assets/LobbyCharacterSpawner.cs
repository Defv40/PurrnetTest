using UnityEngine;

public class LobbyCharacterSpawner : MonoBehaviour
{
    public GameObject SpawnCharacter(GameObject character, Transform parent)
    {
        return Instantiate(character, parent.position, Quaternion.identity, parent);
    }

    public void DespawnCharacter(GameObject character)
    {
        Destroy(character);
    }
}

using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] RoomSpawner roomSpawner;
    [SerializeField] Map debugMap;
    [SerializeField] Map randomMap;
    [SerializeField] GameObject player;

    void Start()
    {
        // TODO: Replace with proc. gen map
        //roomSpawner.SpawnFromMap(debugMap);
        randomMap = MapGenerator.RandomMap(10, 10, 10, 20, 0.2f, 0.2f);
        roomSpawner.SpawnFromMap(randomMap);
        player.transform.position = roomSpawner.StartRoom.transform.position;
    }
}
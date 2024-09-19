using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] RoomSpawner roomSpawner;
    [SerializeField] Map debugMap;
    [SerializeField] Map randomMap;
    [SerializeField] GameObject player;
    [SerializeField] SceneTransition sceneTransition;

    void Start()
    {
        // TODO: Replace with proc. gen map
        //roomSpawner.SpawnFromMap(debugMap);
        randomMap = MapGenerator.RandomMap(10, 10, 10, 20, 0.2f, 0.2f);
        roomSpawner.SpawnFromMap(randomMap);
        player.transform.position = roomSpawner.StartRoom.transform.position;
    }

    public void Restart()
    {
        // Restart current floor. Destroys player. I don't know how future scenes will be futureproofed so I'm putting in destroy and instantiation lines.
        Destroy(player);
        sceneTransition.ReloadScene();

    }

    public void GoToNext()
    {
        // Restart current floor. DO NOT reset player.

    }
}
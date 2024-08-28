using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public enum RoomContents {
    Normal,
    Empty,
    Boss,
    Item,
}

[System.Flags, System.Serializable]
public enum DoorPositionMask {
    Left = 1,
    Right = 2,
    Top = 4,
    Bottom = 8,

    Horizontal = Left | Right,
    Vertical = Top | Bottom,
    All = Horizontal | Vertical,
}

public class Room : MonoBehaviour
{
    const string PLAYER_TAG = "Player";

    [SerializeField] public UnityEvent PlayerEnteredRoomEvent;
    [SerializeField] public UnityEvent PlayerFirstEnteredRoomEvent;

    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] Wall leftWall;
    [SerializeField] Wall rightWall;
    [SerializeField] Wall topWall;
    [SerializeField] Wall bottomWall;

    [SerializeField] List<RoomLayoutPool> layoutPools;

    // Set by RoomSpawner when spawning rooms from a map
    // this is the index in the Map that the room
    // was spawned from.
    // Roughly equivalent to [round(position.x / 24), round(position.y / 18)]
    [System.NonSerialized] public Vector2Int Location;

    [System.NonSerialized] public RoomContents Contents;
    [System.NonSerialized] public RoomLayout Layout;

    bool active = false;
    bool previouslyEntered = false;

    public void InitializeLayout(DoorPositionMask doorPositions){
        leftWall.SetDoorEnabled((doorPositions & DoorPositionMask.Left) > 0);
        rightWall.SetDoorEnabled((doorPositions & DoorPositionMask.Right) > 0);
        topWall.SetDoorEnabled((doorPositions & DoorPositionMask.Top) > 0);
        bottomWall.SetDoorEnabled((doorPositions & DoorPositionMask.Bottom) > 0);

        var randomLayoutPrefab = GetRandomLayoutPrefab(doorPositions);
        var randomLayoutGO = Instantiate(randomLayoutPrefab, transform);
        Layout = randomLayoutGO.GetComponent<RoomLayout>();
    }

    public void CloseDoors(){
        leftWall.SetDoorOpen(false);
        rightWall.SetDoorOpen(false);
        topWall.SetDoorOpen(false);
        bottomWall.SetDoorOpen(false);
    }

    public void OpenDoors(){
        leftWall.SetDoorOpen(true);
        rightWall.SetDoorOpen(true);
        topWall.SetDoorOpen(true);
        bottomWall.SetDoorOpen(true);
    }

    void Start(){
        foreach(var wall in new Wall[] { leftWall, rightWall, topWall, bottomWall }) {
            wall.PlayerExitedWallEvent.AddListener(OnPlayerExitWall);
        }
    }

    GameObject GetRandomLayoutPrefab(DoorPositionMask doorPositions){
        List<RoomLayoutPool> validPools = new();
        int totalLayouts = 0;

        foreach(var pool in layoutPools){
            // if the pool is valid for all the door positions
            // this room has, add it to the list
            if((pool.DoorPositions & doorPositions) == doorPositions){
                validPools.Add(pool);
                totalLayouts += pool.RoomLayoutPrefabs.Count;
            }
        }

        // instead of getting a random pool then getting a
        // random layout within the pool (which would have higher
        // probability for layouts in smaller pools)
        // select a random layout index from across all pools
        int layoutIndex = Random.Range(0, totalLayouts);
        foreach(var pool in validPools) {
            if(layoutIndex < pool.RoomLayoutPrefabs.Count){
                return pool.RoomLayoutPrefabs[layoutIndex];
            }
            layoutIndex -= pool.RoomLayoutPrefabs.Count;
        }
        Debug.LogError("Could not get random layout prefab! (Either there are no layouts, or this is a bug.)");
        return null;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag(PLAYER_TAG)) {
            vcam.enabled = true;
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag(PLAYER_TAG)) {
            active = false;
            vcam.enabled = false;
        }
    }
    void OnPlayerExitWall(){
        if(active){
            PlayerEnteredRoomEvent.Invoke();
            if(!previouslyEntered){
                PlayerFirstEnteredRoomEvent.Invoke();
                previouslyEntered = true;
            }
        }
    }
}

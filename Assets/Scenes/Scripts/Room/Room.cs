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

    // Set by RoomSpawner when spawning rooms from a map
    // this is the index in the Map that the room
    // was spawned from.
    // Roughly equivalent to [round(position.x / 24), round(position.y / 18)]
    [System.NonSerialized] public Vector2Int Location;

    [System.NonSerialized] public RoomContents Contents;

    bool active = false;
    bool previouslyEntered = false;

    public void InitializeDoors(bool leftDoor, bool rightDoor, bool topDoor, bool bottomDoor){
        leftWall.SetDoorEnabled(leftDoor);
        rightWall.SetDoorEnabled(rightDoor);
        topWall.SetDoorEnabled(topDoor);
        bottomWall.SetDoorEnabled(bottomDoor);
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

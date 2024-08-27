using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    const string PLAYER_TAG = "Player";

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

    bool active = false;
    bool playerInDoor = false;

    public void InitializeDoors(bool leftDoor, bool rightDoor, bool topDoor, bool bottomDoor){
        leftWall.SetSprite(leftDoor ? Wall.WallSprite.Door : Wall.WallSprite.Default);
        rightWall.SetSprite(rightDoor ? Wall.WallSprite.Door : Wall.WallSprite.Default);
        topWall.SetSprite(topDoor ? Wall.WallSprite.Door : Wall.WallSprite.Default);
        bottomWall.SetSprite(bottomDoor ? Wall.WallSprite.Door : Wall.WallSprite.Default);
    }

    void Start(){
        foreach(var wall in new Wall[] { leftWall, rightWall, topWall, bottomWall }) {
            wall.PlayerEnteredWallEvent.AddListener(OnPlayerEnterWall);
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

    public void OnPlayerEnterWall(){
        playerInDoor = true;
    }

    public void OnPlayerExitWall(){
        playerInDoor = false;
        if(active){
            Debug.Log("START ROOM");
        }
    }
}

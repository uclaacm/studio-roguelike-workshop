using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public enum RoomContents
{
    Normal,
    Empty,
    Boss,
    Item,
}

[System.Flags, System.Serializable]
public enum DoorPositionMask
{
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

    [SerializeField] RoomLayoutPoolSO normalLayoutPool;
    [SerializeField] RoomLayoutPoolSO bossLayoutPool;

    // Set by RoomSpawner when spawning rooms from a map
    // this is the index in the Map that the room
    // was spawned from.
    // Roughly equivalent to [round(position.x / 24), round(position.y / 18)]
    [System.NonSerialized] public Vector2Int Location;

    [System.NonSerialized] public RoomContents Contents;
    [System.NonSerialized] public RoomLayout Layout;

    bool active = false;
    bool previouslyEntered = false;

    public void InitializeLayout(DoorPositionMask doorPositions)
    {
        leftWall.SetDoorEnabled((doorPositions & DoorPositionMask.Left) > 0);
        rightWall.SetDoorEnabled((doorPositions & DoorPositionMask.Right) > 0);
        topWall.SetDoorEnabled((doorPositions & DoorPositionMask.Top) > 0);
        bottomWall.SetDoorEnabled((doorPositions & DoorPositionMask.Bottom) > 0);

        RoomLayoutPoolSO layoutPool = normalLayoutPool;
        if(Contents == RoomContents.Boss){
            layoutPool = bossLayoutPool;
        }
        var randomLayoutPrefab = layoutPool.GetRandomLayoutPrefab(doorPositions);
        var randomLayoutGO = Instantiate(randomLayoutPrefab, transform);
        Layout = randomLayoutGO.GetComponent<RoomLayout>();
    }

    public void CloseDoors()
    {
        leftWall.SetDoorOpen(false);
        rightWall.SetDoorOpen(false);
        topWall.SetDoorOpen(false);
        bottomWall.SetDoorOpen(false);
    }

    public void OpenDoors()
    {
        leftWall.SetDoorOpen(true);
        rightWall.SetDoorOpen(true);
        topWall.SetDoorOpen(true);
        bottomWall.SetDoorOpen(true);
    }

    void Start()
    {
        foreach (var wall in new Wall[] { leftWall, rightWall, topWall, bottomWall })
        {
            wall.PlayerExitedWallEvent.AddListener(OnPlayerExitWall);
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(PLAYER_TAG))
        {
            vcam.enabled = true;
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag(PLAYER_TAG))
        {
            active = false;
            vcam.enabled = false;
        }
    }
    void OnPlayerExitWall()
    {
        if (active)
        {
            PlayerEnteredRoomEvent.Invoke();
            if (!previouslyEntered)
            {
                PlayerFirstEnteredRoomEvent.Invoke();
                previouslyEntered = true;
            }
        }
    }
}

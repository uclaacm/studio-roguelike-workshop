using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomLayout : MonoBehaviour
{
    public DoorPositionMask DoorPositions;
    public List<Transform> EnemySpawnPoints;
    public Transform ItemSpawnPoint;

    [SerializeField] public bool Debug;
    [SerializeField] public TextMeshPro RoomTypeText;
}
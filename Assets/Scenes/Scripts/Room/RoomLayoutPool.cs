using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomLayoutPool", menuName = "Room Layout Pool")]
public class RoomLayoutPool : ScriptableObject {
	public DoorPositionMask DoorPositions;
	public List<GameObject> RoomLayoutPrefabs;
}
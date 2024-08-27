using UnityEngine;

public class Floor : MonoBehaviour {
	[SerializeField] RoomSpawner roomSpawner;
	[SerializeField] Map debugMap;
	[SerializeField] GameObject player;

	void Start(){
		// TODO: Replace with proc. gen map
		roomSpawner.SpawnFromMap(debugMap);
		player.transform.position = roomSpawner.StartRoom.transform.position;
	}
}
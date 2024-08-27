using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
	[SerializeField] Room roomPrefab;

	// this is the distance between adjacent rooms
	// in unity units.
	[SerializeField] Vector2 stride = new(24, 18);

	// these are calculated after SpawnFromMap is called
	[System.NonSerialized] public List<Room> Rooms = new();
	[System.NonSerialized] public Room StartRoom = null;
	[System.NonSerialized] public Room BossRoom = null;
	[System.NonSerialized] public Room ItemRoom = null;

	public void SpawnFromMap(Map map){
		if(Rooms.Count > 0){
			Debug.LogError("Tried to spawn multiple maps.");
			return;
		}

		for(int x = 0; x < map.Width; ++x){
			for(int y = 0; y < map.Height; ++y){
				MapEntry entry = map.Get(x, y);
				if(entry.IsEmpty){
					continue;
				}

				bool leftEmpty = x == 0 || map.Get(x - 1, y).IsEmpty;
				bool rightEmpty = x == map.Width - 1 || map.Get(x + 1, y).IsEmpty;
				bool topEmpty = y == 0 || map.Get(x, y - 1).IsEmpty;
				bool bottomEmpty = y == map.Height - 1 || map.Get(x, y + 1).IsEmpty;

				var roomGO = Instantiate(
					roomPrefab.gameObject,
					transform
				);

				roomGO.transform.position = new Vector2(
					stride.x * x,
					stride.y * -y
				);

				var room = roomGO.GetComponent<Room>();
				room.InitializeDoors(
					leftDoor: !leftEmpty,
					rightDoor: !rightEmpty,
					topDoor: !topEmpty,
					bottomDoor: !bottomEmpty
				);

				room.Location = new(x, y);

				Rooms.Add(room);

				if(entry.Type == MapEntryType.StartRoom) {
					if(StartRoom){
						Debug.LogWarning("Multiple start rooms");
					}
					StartRoom = room;
				}
				else if(entry.Type == MapEntryType.BossRoom) {
					if(BossRoom){
						Debug.LogWarning("Multiple boss rooms");
					}
					BossRoom = room;
				}
				else if(entry.Type == MapEntryType.ItemRoom) {
					if(ItemRoom){
						Debug.LogWarning("Multiple item rooms");
					}
					ItemRoom = room;
				}
			}
		}
	}
}
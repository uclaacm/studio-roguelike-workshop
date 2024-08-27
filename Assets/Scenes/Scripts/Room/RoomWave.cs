using UnityEngine;

class RoomWave : MonoBehaviour {
	[SerializeField] Room room;

	void Start(){
		// dont spawn enemies unless its a default room
		if(room.Contents != RoomContents.Normal) {
			return;
		}
		room.PlayerFirstEnteredRoomEvent.AddListener(StartWave);
	}

	void StartWave(){
		room.CloseDoors();
	}

	void EndWave(){
		room.OpenDoors();
	}
}
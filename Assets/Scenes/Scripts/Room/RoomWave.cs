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
		Debug.Log("Start wave");
		// room.CloseDoors();
		foreach(var spawnPoint in room.Layout.EnemySpawnPoints){
			SpawnRandomEnemy(spawnPoint);
		}
	}

	void EndWave(){
		// room.OpenDoors();
	}

	void SpawnRandomEnemy(Transform spawnPoint){
		// todo
	}
}
using UnityEngine;

class RoomWave : MonoBehaviour {
	[SerializeField] Room room;
	[SerializeField] EnemyPoolSO enemyPool;
	[SerializeField] EnemyPoolSO bossPool;

	int enemiesRemaining = 0;

	void Start(){
		// dont spawn enemies unless its a default room
		if(room.Contents == RoomContents.Normal) {
			room.PlayerFirstEnteredRoomEvent.AddListener(StartEnemyWave);
		}
		else if(room.Contents == RoomContents.Boss) {
			room.PlayerFirstEnteredRoomEvent.AddListener(StartBossWave);
		}
	}

	void StartEnemyWave(){
		room.CloseDoors();
		foreach(Transform spawnPoint in room.Layout.EnemySpawnParent){
			SpawnRandomEnemy(spawnPoint);
		}
	}

	void StartBossWave(){
		room.CloseDoors();
		// TODO:
	}

	void EndWave(){
		room.OpenDoors();
	}

	void SpawnRandomEnemy(Transform spawnPoint){
		enemiesRemaining++;

		int index = Random.Range(0, enemyPool.EnemyPrefabs.Count - 1);
		var enemyPrefab = enemyPool.EnemyPrefabs[index];
		var enemyGO = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
		var enemy = enemyGO.GetComponent<Entity>();
		enemy.DeathEvent.AddListener(OnEnemyDeath);
	}

	void OnEnemyDeath(){
		enemiesRemaining--;
		if(enemiesRemaining == 0){
			EndWave();
		}
	}
}
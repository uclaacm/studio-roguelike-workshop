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
			SpawnRandomEnemy(enemyPool, spawnPoint);
		}
	}

	void StartBossWave(){
		room.CloseDoors();
		SpawnRandomEnemy(enemyPool, room.Layout.EnemySpawnParent.GetChild(0));
	}

	void EndWave(){
		room.OpenDoors();
		// TODO: Open portal to next floor
	}

	void SpawnRandomEnemy(EnemyPoolSO pool, Transform spawnPoint){
		enemiesRemaining++;

		int index = Random.Range(0, pool.EnemyPrefabs.Count - 1);
		var enemyPrefab = pool.EnemyPrefabs[index];
		SpawnEnemy(enemyPrefab, spawnPoint);
	}

	void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint){
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
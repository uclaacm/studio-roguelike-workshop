using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomLayoutPool", menuName = "Room Layout Pool")]
public class RoomLayoutPoolSO : ScriptableObject {
	public List<GameObject> RoomLayoutPrefabs;

	SortedDictionary<DoorPositionMask, List<GameObject>> layoutsPrefabsByDoorPositions = new();

    // groups each roomlayout by its door position mask
	void OnEnable(){
		foreach(var roomLayoutGO in RoomLayoutPrefabs){
			var room = roomLayoutGO.GetComponent<RoomLayout>();
            if (!layoutsPrefabsByDoorPositions.ContainsKey(room.DoorPositions)) {
                layoutsPrefabsByDoorPositions[room.DoorPositions] = new();
            }
			layoutsPrefabsByDoorPositions[room.DoorPositions].Add(roomLayoutGO);
		}
	}

    // Gets a random RoomLayout whose DoorPositions is a superset of doorPositions
	public GameObject GetRandomLayoutPrefab(DoorPositionMask doorPositions){
        List<List<GameObject>> validPools = new();
        int totalLayouts = 0;

        foreach(var item in layoutsPrefabsByDoorPositions){
			var poolDoorPositions = item.Key;
			var poolPrefabs = item.Value;

            // if the pool is valid for all the door positions
            if((poolDoorPositions & doorPositions) == doorPositions){
                validPools.Add(poolPrefabs);
                totalLayouts += poolPrefabs.Count;
            }
        }

        // instead of getting a random pool then getting a
        // random layout within the pool (which would have higher
        // probability for layouts in smaller pools)
        // select a random layout index from across all pools
        int layoutIndex = Random.Range(0, totalLayouts);
        Debug.Log(layoutIndex);
        foreach(var pool in validPools) {
            if(layoutIndex < pool.Count){
                return pool[layoutIndex];
            }
            layoutIndex -= pool.Count;
        }
        Debug.LogError($"Could not get random layout prefab! (Either there are no layouts, or this is a bug. doorPositions: {doorPositions})");
        return null;
    }

}
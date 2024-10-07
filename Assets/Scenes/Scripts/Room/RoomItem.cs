using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] Room room;

    [SerializeField] ItemPoolSO itemPool;

    static List<ItemSO> itemPoolRemaining = null;

    void Start(){
        if(room.Contents != RoomContents.Item){
            return;
        }
        room.PlayerFirstEnteredRoomEvent.AddListener(OnPlayerEnterRoom);

        if(itemPoolRemaining == null){
            itemPoolRemaining = new List<ItemSO>(itemPool.Items);
        }
    }

    void OnPlayerEnterRoom(){
        SpawnItem();
    }

    void SpawnItem(){
        // ran out of items :(
        if(itemPoolRemaining.Count == 0) {
            Debug.Log("Ran out of items");
            return;
        }
        int itemIndex = Random.Range(0, itemPoolRemaining.Count);
        var item = itemPool.Items[itemIndex];
        Instantiate(item.pickupPrefab, transform.position, Quaternion.identity);

        itemPoolRemaining.RemoveAt(itemIndex);
    }
}

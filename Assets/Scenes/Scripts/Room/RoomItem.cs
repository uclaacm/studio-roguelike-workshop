using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] Room room;

    [SerializeField] ItemPoolSO itemPool;

    void Start(){
        if(room.Contents != RoomContents.Item){
            return;
        }
        room.PlayerFirstEnteredRoomEvent.AddListener(OnPlayerEnterRoom);
    }

    void OnPlayerEnterRoom(){
        SpawnItem();
    }

    void SpawnItem(){
        int itemIndex = Random.Range(0, itemPool.Items.Count - 1);
        var item = itemPool.Items[itemIndex];
        Instantiate(item.pickupPrefab, transform.position, Quaternion.identity);
    }
}

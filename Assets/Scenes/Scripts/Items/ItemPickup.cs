using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(itemSO.itemPrefab, other.gameObject.transform);
            ItemNotificationUI.Instance.ShowPickupNotification(itemSO);
            Destroy(gameObject);
        }
    }
}

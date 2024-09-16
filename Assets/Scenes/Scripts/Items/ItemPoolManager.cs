using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    private static ItemPoolManager itemPMInstance;
    public static ItemPoolManager Instance { get { return itemPMInstance; } }
    public HashSet<ItemSO> itemSOPool;
    private void Awake()
    {
        if (itemPMInstance != null && itemPMInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            itemPMInstance = this;
        }
    }
}

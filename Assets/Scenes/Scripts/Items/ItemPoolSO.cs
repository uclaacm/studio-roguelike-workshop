using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemPool", menuName = "Item Pool")]
public class ItemPoolSO : ScriptableObject
{
    [SerializeField] public List<ItemSO> Items;
}

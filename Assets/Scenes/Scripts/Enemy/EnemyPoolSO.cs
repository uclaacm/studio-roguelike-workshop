using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "Enemy Pool")]
public class EnemyPoolSO : ScriptableObject
{
    [SerializeField] public List<GameObject> EnemyPrefabs = new();
}

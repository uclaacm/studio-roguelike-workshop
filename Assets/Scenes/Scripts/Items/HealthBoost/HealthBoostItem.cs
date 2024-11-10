using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostItem : MonoBehaviour
{
    [SerializeField] float healthDelta = 1f;

    void Start(){
        var entity = transform.parent.GetComponent<Entity>();
        entity.stats.MaxHealth += healthDelta;
        entity.CurrentHealth += healthDelta;
    }
}

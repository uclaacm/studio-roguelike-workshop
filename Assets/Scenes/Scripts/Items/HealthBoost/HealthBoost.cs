using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [SerializeField] float healthDelta = 1.0f;

    void Start()
    {
        Entity entity = transform.parent.GetComponent<Entity>();
        // TODO: increase current and max health by healthDelta
    }
}

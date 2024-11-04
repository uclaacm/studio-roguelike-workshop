using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelCollider : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Floor.Instance.GoToNext();
        }
    }
}

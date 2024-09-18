using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetHealth(int health) {
        animator.SetBool("half", health == 1);
        animator.SetBool("full", health == 2);
    }
}

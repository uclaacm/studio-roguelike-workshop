using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNotificationUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] string format = "You picked up <color=\"blue\">{0}</color>";
    [SerializeField] Animator animator;

    public static ItemNotificationUI Instance;

    void Awake(){
        Instance = this;
    }

    public void ShowPickupNotification(ItemSO item){
        text.text = string.Format(format, item.itemName);
        animator.SetTrigger("Shown");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour
{
    [SerializeField] Collider2D doorCollider;
    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject wallWithDoorSprite;
    [SerializeField] GameObject wallWithoutDoorSprite;

    [SerializeField] public UnityEvent PlayerEnteredWallEvent;
    [SerializeField] public UnityEvent PlayerExitedWallEvent;

    bool doorEnabled = false;

    public void SetDoorEnabled(bool enabled){
        doorEnabled = enabled;

        if (!enabled){
            wallWithDoorSprite.SetActive(false);
            wallWithoutDoorSprite.SetActive(true);
        }
        else {
            wallWithDoorSprite.SetActive(true);
            wallWithoutDoorSprite.SetActive(false);
            SetDoorOpen(true);
        }
    }

    public void SetDoorOpen(bool open){
        if(!doorEnabled) return;
        doorCollider.enabled = !open;
        doorAnimator.SetBool("open", open);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            PlayerEnteredWallEvent.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            PlayerExitedWallEvent.Invoke();
        }
    }
}

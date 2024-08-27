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

    public enum WallSprite {
        Default,
        Door,
    }

    void Awake(){
        SetDoorOpen(true);
    }

    public void SetSprite(WallSprite wallSprite){
        if(wallSprite == WallSprite.Default){
            wallWithDoorSprite.SetActive(false);
            wallWithoutDoorSprite.SetActive(true);
        }
        else if(wallSprite == WallSprite.Door) {
            wallWithDoorSprite.SetActive(true);
            wallWithoutDoorSprite.SetActive(false);
        }
    }

    public void SetDoorOpen(bool open){
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPause : MonoBehaviour
{
    [SerializeField] PauseMenuUI pauseMenuUI;

    void OnPause(InputValue v){
        if(v.isPressed){
            pauseMenuUI.Pause();
        }
    }
}

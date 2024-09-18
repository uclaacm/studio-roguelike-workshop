using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    Canvas canvas;

    void Start(){
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        Player.Instance.DeathEvent.AddListener(OnPlayerDeath);
    }

    void OnPlayerDeath(){
        canvas.enabled = true;
    }
}
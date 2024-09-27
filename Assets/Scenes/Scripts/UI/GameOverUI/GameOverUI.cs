using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] string menuScene = "Menu";

    Canvas canvas;

    void Start(){
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        Player.Instance.DeathEvent.AddListener(OnPlayerDeath);
    }

    void OnPlayerDeath(){
        canvas.enabled = true;
    }

    public void OnRestartPressed(){
        GameManager.Instance.Restart();
    }

    public void OnExitToMenuPressed(){
        GameManager.Instance.GoToMenu();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] string menuScene;

    public static bool isPaused = false;

    Canvas canvas;

    void Awake(){
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        isPaused = false;
    }

    public void Pause(){
        Time.timeScale = 0;
        canvas.enabled = true;
        isPaused = true;
    }

    public void OnResumePressed(){
        Time.timeScale = 1;
        canvas.enabled = false;
        isPaused = false;
    }

    public void OnExitToMenuPressed(){
        Time.timeScale = 1;
        SceneTransition.Instance.LoadScene(menuScene);
    }
}

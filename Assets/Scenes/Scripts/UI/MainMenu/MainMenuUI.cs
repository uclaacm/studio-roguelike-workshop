using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] string gameScene;

    public void OnStartPressed(){
        SceneTransition.Instance.LoadScene(gameScene);
    }

    public void OnQuitPressed(){
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(0);
#endif
    }
}

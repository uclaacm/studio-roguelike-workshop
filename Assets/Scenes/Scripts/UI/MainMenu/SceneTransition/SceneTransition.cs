using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Animator animator;

    public static SceneTransition Instance = null;

    AsyncOperation asyncOperation = null;

    void Awake(){
        if (Instance != null){
            Destroy(gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(string name){
        asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false;

        animator.SetTrigger("transition_in");
    }

    public void ReloadScene(){
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SceneTransitionReady(){
        asyncOperation.completed += SceneTransitionComplete;
        asyncOperation.allowSceneActivation = true;
    }

    public void SceneTransitionComplete(AsyncOperation _){
        animator.SetTrigger("transition_out");
        asyncOperation = null;
    }
}

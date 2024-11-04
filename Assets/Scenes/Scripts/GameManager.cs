using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] string menuScene;

    public static GameManager Instance;

    void Awake(){
        if(Instance){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    void Start(){
		DontDestroyOnLoad(Player.Instance.gameObject);
    }

    void DestroyPersistentObjects(){
        var activeScene = SceneManager.GetActiveScene();
        if(Player.Instance){
            SceneManager.MoveGameObjectToScene(Player.Instance.gameObject, activeScene);
        }
        SceneManager.MoveGameObjectToScene(gameObject, activeScene);
    }

    public void Restart(){
        DestroyPersistentObjects();
    }

    public void GoToMenu(){
        DestroyPersistentObjects();
    }
}

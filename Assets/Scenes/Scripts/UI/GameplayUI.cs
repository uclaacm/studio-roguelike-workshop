using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;

    void Awake(){
        if(Instance){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}

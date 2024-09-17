using UnityEngine;

public class Player : MonoBehaviour {
	public static Entity Instance;

	void Awake(){
		if(Instance){
			Debug.LogError("Multiple player instances!");
			Destroy(gameObject);
		}
		Instance = GetComponent<Entity>();
	}
}
using UnityEngine;

public class Player : MonoBehaviour {
	public static Entity Instance;

	public static bool IsDead => !Instance;

	void Awake(){
		if(Instance){
			Debug.LogError("Multiple player instances!");
			Destroy(gameObject);
		}
		Instance = GetComponent<Entity>();
	}
}
using UnityEngine;

public class Player : MonoBehaviour {
	public static Entity Instance;

	public static bool IsDead => !Instance;

	void Awake(){
		if(Instance){
			Destroy(gameObject);
			return;
		}
		Instance = GetComponent<Entity>();
	}
}
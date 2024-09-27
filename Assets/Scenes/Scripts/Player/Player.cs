using UnityEngine;

public class Player : MonoBehaviour {
	public static Entity Instance;

	public static bool IsDead => !Instance;

	void Awake(){
		Debug.Log($"AWAKE: {gameObject.GetInstanceID()}");
		if(Instance){
			Destroy(gameObject);
			return;
		}
		Instance = GetComponent<Entity>();
	}
}
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour {
	Weapon weapon;

	bool isColliding = false;
	float lastDamageTime = float.NegativeInfinity;
	Entity player;

	void Start() {
		weapon = GetComponent<Weapon>();
		player = Player.Instance;
	}

	void Update(){
		if(isColliding && !Player.IsDead && Time.time > lastDamageTime + weapon.ShootingCooldown){
			player.TakeDamage(weapon.Damage);
			lastDamageTime = Time.time;
		}
	}


	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.CompareTag("Player")){
			isColliding = true;
		}
	}


	void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject.CompareTag("Player")){
			isColliding = false;
		}
	}
}
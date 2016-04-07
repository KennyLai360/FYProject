using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	
	void Start() {
		//help recording the score.
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("ERROR: GAME CONTROLLER NOT FOUND.");
		}
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Boundary" || other.tag == "Enemy") {
			return;
		};	
			
		//If the contact is with the object with the Player tag, then it will instantiate a playerExplosion
		//at the same spot where the collision happened.
		if (other.tag == "Player") {
		Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		gameController.GameOver();
		}
		
		//This applies to the Enemy projectiles.
		if (explosion != null) {
			//if not explosion is not null meaning it's an asteroid or enemy, then instantiate the explosion.
			Instantiate(explosion,transform.position, transform.rotation);
			Destroy(other.gameObject);
			Destroy(gameObject);
		} else if (other.tag == "Player") {
			//Else if explosion is null and it hits the player tag then destroy it.
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
		
		gameController.AddScore(scoreValue);
		
	}
}

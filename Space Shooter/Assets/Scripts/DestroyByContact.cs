using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	
	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("ERROR: GAME CONTROLLER NOT FOUND.");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		};
		
		//If the contact is with the object with the Player tag, then it will instantiate a playerExplosion
		//at the same spot where the collision happened.
		if (other.tag == "Player") {
		Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		gameController.GameOver();
		}
		
		gameController.AddScore(scoreValue);
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(other.gameObject);
		Destroy(gameObject);
		
	}
}

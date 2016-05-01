using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public GameObject protectPlayerExplosion;
	public GameObject protectExplosion;
	public int scoreValue;
	private GameController gameController;
	private PlayerController playerController;
	public int objectLifes;
	
	void Start() {
		//Just to make sure the player's projectiles are detroyed if it hits something.
		//Otherwise if it's not a projectile, then Apply a life between 0-2 for that object,
		//it will take from 0-2 hits of the player's projectile to destroy the object.
		if (explosion == null) {
			objectLifes = 0;
		} else {
			objectLifes = Random.Range(0, 2);
		}
		//help recording the score.
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("ERROR: GAME CONTROLLER NOT FOUND.");
		}
		
		GameObject playerControllerObject = GameObject.FindWithTag("Player");
		//If the playerController is null then most likely the player has been destroyed and the 
		//game has ended.
		if (playerControllerObject != null) {
			playerController = playerControllerObject.GetComponent<PlayerController>();
		}
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Boundary" || other.tag == "Enemy") {
			return;
		};
			
		//If the contact is with the object with the Player tag, then it will instantiate a playerExplosion
		//at the same spot where the collision happened.
		if (other.tag == "Player" && playerController.IsInvul() == false) {
		playerController.DecreaseLife();
		if (playerController.LifesRemaining() == 0) {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		} else {
			//If the player is hit by something then instantiate the protection explosion and set player Invulnerable
			//for 1.5 seconds.
			Instantiate(protectPlayerExplosion, other.transform.position, other.transform.rotation);
			playerController.SetInvul();
		}
		
		}
		
		//This applies to the Enemy projectiles.
		if (explosion != null && protectExplosion != null) {
			if (playerController.LifesRemaining() == 0) {
				Destroy(other.gameObject);
			}
			//If the playerbolt comes in contact with anything we want to destroy the player bolt.
			if (other.tag == "PlayerBolt") {
				Destroy(other.gameObject);
			}
			if(objectLifes == 0) {
				//if not explosion is not null meaning it's an asteroid or enemy, then instantiate the explosion.
				Instantiate(explosion,transform.position, transform.rotation);
				Destroy(gameObject);
				gameController.AddScore(scoreValue);
			} else {
				Instantiate(protectExplosion, other.transform.position, other.transform.rotation);
				objectLifes--;
			}
			
		} else if (other.tag == "Player") {
			//Else if explosion is null and it hits the player tag then destroy it. Meaning it came in contact with the
			//enemies fired bolt, then this part of the if statement would be executed.
			if (playerController.LifesRemaining() == 0 && playerController.IsInvul() == false) {
				Destroy(other.gameObject);
			}
			if(objectLifes == 0) {
				Destroy(gameObject);
				gameController.AddScore(scoreValue);
			} else {
				objectLifes--;
				Destroy(gameObject);
			}
		}
		
	}
}

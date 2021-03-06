﻿using UnityEngine;
using System.Collections;

//Moved the restriction values to another class so it's neater in the actual game editor.
[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour {
	
	public Boundary boundary;
	
	private Rigidbody rb;
	public float movementFactor;
	public float playerTilt;
	private int lifes;
	
	public GameObject projectile;
	public Transform[] projectileSpawns;
	public float fireRate;
	private float fixedFireRateIncrease = 0.05f;
	private float nextFire;
	public int projectileLevel;
	private int[] projectileLevels;
	private bool invul;
	
	private AudioSource au;
	private GameController gameController;
	
	void Start() {
		invul = false;
		lifes = 3;
		projectileLevel = 1;
		projectileLevels = new int[] {0, 1, 3};
		
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("ERROR: GAME CONTROLLER NOT FOUND.");
		}
		
		rb = GetComponent<Rigidbody>();
	}
	
	public void IncreaseFireRate() {
		fireRate -= fixedFireRateIncrease;
	}
	
	public void IncreaseProjectileLevel() {
		projectileLevel++;
	}
	
	public void DecreaseLife() {
		lifes--;
	}
	
	public int LifesRemaining() {
		return lifes;
	}
	
	public Vector3 PlayerCurrentPosition() {
		return rb.position;
	}
	
	public bool IsInvul() {
		return invul;
	}
	
	public void SetInvul() {
		StartCoroutine(setInvulnerable());
	}
	
	IEnumerator setInvulnerable() {
		invul = true;
		yield return new WaitForSeconds(1.5f);
		invul = false;
	}

	
	//Update called right before every frame within the game is loaded.
	void Update() {
		if (!gameController.IsGamePaused()) {
			if (Input.GetButton("Fire1") && Time.time > nextFire) {
				au = GetComponent<AudioSource>();
				nextFire = Time.time + fireRate;
				//Creates an object of the projectile, with the position and rotation of the projectileSpawn.
				for (int i = 0; i < projectileLevels[projectileLevel]; i++) {
				Instantiate(projectile, projectileSpawns[i].position, projectileSpawns[i].rotation);
				};
				au.Play();
			}
		}
	}

	void FixedUpdate() {
		
		//Grab the keyboard inputs to move horizontal and vertical.
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		//Apply the movement values(0 - 1) and apply some velocity to the rigit body.
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * movementFactor;
		
		//Clamp the range of values in the X,Y and Z axis of where the player can move on the screen.
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);
		
		//Rotates the player.
		//Fixed at those points and rotate at the Z axis
		//When the velocity of the object is moving in the Z axis
		//PlayerTilt is the mutlipler of how much it will tilt by.
		rb.rotation = Quaternion.Euler(0.0f, 90f, rb.velocity.z * playerTilt);
		
		/*if (Time.time > 20f) {
			fireRate = 0.1f;
		}*/
	}
	

}

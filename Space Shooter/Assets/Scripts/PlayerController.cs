using UnityEngine;
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
	
	public GameObject projectile;
	public Transform projectileSpawn;
	public float fireRate;
	private float nextFire;
	
	//Update called right before every frame within the game is loaded.
	void Update() {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			//Creates an object of the projectile, with the position and rotation of the projectileSpawn.
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
		}
	}

	void FixedUpdate() {
		
		//Grab the keyboard inputs to move horizontal and vertical.
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		rb = GetComponent<Rigidbody>();
		
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
		rb.rotation = Quaternion.Euler(0.0f, 90.0f, rb.velocity.z * playerTilt);
		
	}
}

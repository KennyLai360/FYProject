using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour {
	
	private Rigidbody rb;
	public float movementFactor;
	public Boundary boundary;
	public float playerTilt;

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		rb = GetComponent<Rigidbody>();
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * movementFactor;
		
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

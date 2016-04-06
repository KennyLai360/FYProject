using UnityEngine;
using System.Collections;

/*
* Generic moving script for moving any object within the game, moving forwards and backwards(apply negative values).
*/

public class Mover : MonoBehaviour {
	
	private Rigidbody rb;
	public float speed;
	
	// Use this for initialization
	void Start () {
			rb = GetComponent<Rigidbody>();
			rb.velocity = transform.forward * speed;
	}
	
}

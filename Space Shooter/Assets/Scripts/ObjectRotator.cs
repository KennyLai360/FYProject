using UnityEngine;
using System.Collections;

public class ObjectRotator : MonoBehaviour {
	
	private Rigidbody rb;
	
	public float tumble;
	
	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
}

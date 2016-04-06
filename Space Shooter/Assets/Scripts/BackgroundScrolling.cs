using UnityEngine;
using System.Collections;

public class BackgroundScrolling : MonoBehaviour {
	
	public float scrollSpeed;
	public float tileSizeZ;
	
	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveBG = new Vector3(1.0f, 0.0f, 0.0f);
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + moveBG * newPosition;
	}
}

using UnityEngine;
using System.Collections;

/*
* Only used by the Boundary game object for when a game object leaves the screen
for example a projectile, it will destroy it. prevent any clogging the system's memory
*/

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}

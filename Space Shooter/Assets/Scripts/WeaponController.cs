using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public GameObject enemyProjectile;
	public Transform enemyShotSpawn;
	public float enemyFireRate;
	public float enemyDelay;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("enemyFire", enemyDelay, enemyFireRate);
	}
	
	void enemyFire() {
		Instantiate(enemyProjectile, enemyShotSpawn.position, enemyShotSpawn.rotation);
	}
}

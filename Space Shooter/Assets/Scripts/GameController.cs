using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnPositionValues;
	
	public int hazardCount;
	public float beginningEnemySpawnWait;
	public float spawnWait;
	public float timeForNextWave;
	
	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnWaves());
	}
	
	IEnumerator SpawnWaves() {
		//Wait for X amount of seconds before spawning the enemy wave so the player can prepare.
		yield return new WaitForSeconds(beginningEnemySpawnWait);
		
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3(spawnPositionValues.x, 0.0f, Random.Range(-spawnPositionValues.z, spawnPositionValues.z));
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(timeForNextWave);
		}
	}
	
}

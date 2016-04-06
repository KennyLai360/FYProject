using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnPositionValues;
	
	public int hazardCount;
	public float beginningEnemySpawnWait;
	public float spawnWait;
	public float timeForNextWave;
	
	private int score;
	public Text scoreText;
	public Text GameOverText;
	public Text retryText;
	
	private bool gameOver;
	private bool restart;
	
	// Use this for initialization
	void Start () {
		gameOver = false;
		restart = false;
		GameOverText.enabled = false;
		retryText.enabled = false;
		score = 0;
		StartCoroutine (SpawnWaves());
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			RestartCurrentScene();
		}
	}
	
	IEnumerator SpawnWaves() {
		//Wait for X amount of seconds before spawning the enemy wave so the player can prepare.
		yield return new WaitForSeconds(beginningEnemySpawnWait);
		
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[(Random.Range(0, hazards.Length))];
				Vector3 spawnPosition = new Vector3(spawnPositionValues.x, 0.0f, Random.Range(-spawnPositionValues.z, spawnPositionValues.z));
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(timeForNextWave);
			
			if (gameOver) {
				retryText.enabled = true;
				restart = true;
				break;
			}
		}
	}
	
	//Public method that can be called anywhere as long as it's found this game controller.
	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}
	
	//Update the GUI text with the new score.
	void UpdateScore() {
		scoreText.text = "Score " + score;
	}
	
	public void GameOver() {
		gameOver = true;
		GameOverText.enabled = true;
	}
	
	void RestartCurrentScene() {
		SceneManager.LoadScene(0);
	}
	
	
	
}

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
	private bool gamePaused;
	
	public GameObject pausePanel;
	public GameObject volumeSlider;
	
	// Use this for initialization
	void Start () {
		Slider slider = volumeSlider.GetComponent<Slider>();
		
		gamePaused = false;
		gameOver = false;
		restart = false;
		GameOverText.enabled = false;
		retryText.enabled = false;
		score = 0;
		
		slider.value = AudioListener.volume;
		
		StartCoroutine (SpawnWaves());
	}
	
	//Used by the playerController to see if the game is paused or not to whether allow the game to 
	//execute the fire method for the player.
	public bool IsGamePaused() {
		if (gamePaused) {
			return true;
		} else {
			return false;
		}
	}
	
	void Update() {
		//As soon as timeScale is set to zero FixedUpdate will not execute.
		if (Time.timeScale == 0) {
			gamePaused = true;
		} else {
			gamePaused = false;
		}
		
		//Gives an option to the palyer to close the Pause menu by pressing the same ESC key.		
		if (Input.GetKeyDown(KeyCode.Escape)) {
				if (gameOver == false && pausePanel.activeSelf == false) {
				Time.timeScale = 0;
				pausePanel.SetActive(true);
				} else if (Input.GetKeyDown(KeyCode.Escape) && restart == true){
				SceneManager.LoadScene(0);
			} else {
				CloseOptionPanel();
			}
		}
		
		if (Input.GetKeyDown(KeyCode.R) && restart == true) {
			SceneManager.LoadScene(1);
		}
	}
	
	/*
	* Method called when the close Options panel is pressed.
	*/
	public void CloseOptionPanel() {
		pausePanel.SetActive(false);
		Time.timeScale = 1;
	}
	
	public void AdjustVolume(float volumeValue) {
		AudioListener.volume = volumeValue;
	}
	
	public void QuitGame() {
		Debug.Log("Game terminated.");
		Application.Quit();
	}
	
	public void ReturnToMainMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
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
			/*hazardCount += 5;
			if (Time.time > 20f) {
				spawnWait = 0.3f;
			}*/
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

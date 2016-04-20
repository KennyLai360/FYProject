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
	public Text highScoreText;
	public Text GameOverText;
	public Text retryText;
	public Text fireRateUpgradableText;
	public Text projectileUpgradableText;
	public Text actionText;
	private int[] fireRateUpgradableCostArray;
	private int[] projectileUpgradableCostArray;
	private Text fireRateButtonText;
	private Text projectileButtonText;
	
	private bool gameOver;
	private bool restart;
	private bool gamePaused;

	
	public GameObject pausePanel;
	public GameObject confirmDialog;
	public GameObject volumeSlider;
	
	public Button fireRateUpgradeButton;
	public Button projectileUpgradeButton;
	
	private PlayerController playerController;
	private int fireRateCount = 0;
	private int projectileCount = 0;
	
	// Use this for initialization
	void Start () {
		Slider slider = volumeSlider.GetComponent<Slider>();
		
		GameObject playerControllerObject = GameObject.FindWithTag("Player");
		if (playerControllerObject != null) {
			playerController = playerControllerObject.GetComponent<PlayerController>();
		} else {
			Debug.Log("ERROR: PLAYER CONTROLLER NOT FOUND.");
		}
		
		gamePaused = false;
		gameOver = false;
		restart = false;
		GameOverText.enabled = false;
		retryText.enabled = false;
		fireRateUpgradableText.text = "1000";
		projectileUpgradableText.text = "5000";
		fireRateUpgradableCostArray = new int[] {1000, 2500, 5000, 10000, 0};
		projectileUpgradableCostArray = new int[] {5000, 0};
		fireRateButtonText = fireRateUpgradeButton.GetComponentInChildren<Text>();
		projectileButtonText = projectileUpgradeButton.GetComponentInChildren<Text>();
		
		score = 0;
		highScoreText.text = ("High score: " + PlayerPrefs.GetInt("highscore", 0).ToString());
		slider.value = AudioListener.volume;
		
		StartCoroutine (SpawnWaves());
	}
	
	/*
	*Press of the upgrade fire rate button, this should increase it and deduct the amount from
	the score. Else if max fire rate is reached then 
	*/
	public void PurchaseFireRateUpgrade() {
		if (score >= fireRateUpgradableCostArray[fireRateCount]) {
			playerController.IncreaseFireRate();
			score = score - fireRateUpgradableCostArray[fireRateCount];
			fireRateCount++;
			if (fireRateUpgradableCostArray[fireRateCount] != 0) {
				fireRateUpgradableText.text = fireRateUpgradableCostArray[fireRateCount].ToString();
			} else {
				fireRateUpgradableText.text = "";
			}
			UpdateScore();
			
			if (playerController.fireRate < 0.06) {
				fireRateUpgradeButton.interactable = false;
				fireRateButtonText.text = "Max fire rate";
			}
			
		}
	}
	
	public bool isGameOver() {
		return gameOver;
	}
	
	void StoreHighscore(int newHighscore) {
		int oldHighscore = PlayerPrefs.GetInt("highscore", 0); 	 
		if(newHighscore > oldHighscore)
        PlayerPrefs.SetInt("highscore", newHighscore);
	}
	
	public void PurchaseIncreaseProjectileUpgrade() {
		if (score >= projectileUpgradableCostArray[projectileCount]) {
			playerController.IncreaseProjectileLevel();
			score = score - projectileUpgradableCostArray[projectileCount];
			projectileCount++;
			if (projectileUpgradableCostArray[projectileCount] != 0) {
				projectileUpgradableText.text = projectileUpgradableCostArray[projectileCount].ToString();
			} else {
				projectileUpgradableText.text = "";
			}
			UpdateScore();
			
			if (playerController.projectileLevel == 2) {
				projectileUpgradeButton.interactable = false;
				projectileButtonText.text = "Max projectiles";
			}
			
		}
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
	
	/*
	*This method has been included in the Update method because we want it to be 
	updated immediately as soon as the user Ugrades something, either the cost text
	should change red or stay red depending on if the player has enough score to spend on it.
	*/
	void updateUpgradeText() {
		if (fireRateUpgradableCostArray[fireRateCount] != 0) {
			if (score >= fireRateUpgradableCostArray[fireRateCount]) {
				fireRateUpgradableText.color = Color.white;
				fireRateUpgradeButton.interactable = true;
			} else {
				fireRateUpgradeButton.interactable = false;
				fireRateUpgradableText.color = Color.red;
			}
		}
		
		if (projectileUpgradableCostArray[projectileCount] != 0) {
			if (score >= projectileUpgradableCostArray[projectileCount]) {
				projectileUpgradableText.color = Color.white;
				projectileUpgradeButton.interactable = true;
			} else {
				projectileUpgradeButton.interactable = false;
				projectileUpgradableText.color = Color.red;
			}
		}
			
			
	}
	
	void Update() {	
			updateUpgradeText();
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
	
	/*
	* In game menu to adjust the master volume.
	*/
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
	
	private int actionTaken;
	
	public void ShowDialog(int value) {
		if (value == 0) {
			actionText.text = "Exit application? You will lose your current game progress!";
			actionTaken = 0;
		} else if (value == 1) {
			actionText.text = "Return to main menu? You will lose your current game progress!";
			actionTaken = 1;
		} else {
			actionText.text = "";
		}
		
		confirmDialog.SetActive(true);
	}
	
	public void CloseDialog() {
		confirmDialog.SetActive(false);
	}
	
	public void YesDialogButtonPressed() {
		if (actionTaken == 0) {
			QuitGame();
		} else if (actionTaken == 1) {
			ReturnToMainMenu();
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
		StoreHighscore(score);
	}
}

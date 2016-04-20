using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	
	public GameObject optionsModal;
	public GameObject volumeSlider;
	public GameObject instructionModal1;
	public GameObject instructionModal2;
	public Text loadingText;
	
	void Start() {
		Slider slider = volumeSlider.GetComponent<Slider>();
		slider.value = AudioListener.volume;
	}
	
	public void StartButtonPress() {
		Debug.Log("START");
		instructionModal1.SetActive(true);
	}
	
	public void OpenOptionsModal() {
		Debug.Log("Opening Options Modal.");
		optionsModal.SetActive(true);
	}
	
	public void CloseOptionsModal() {
		Debug.Log("Closing Options Modal.");
		optionsModal.SetActive(false);
	}
	
	public void ExitGame() {
		Debug.Log("Game Terminated.");
		Application.Quit();
	}
	
	public void AdjustVolume(float newVolumeValue) {
		AudioListener.volume = newVolumeValue;
	}
	
	void Update() {
		if (instructionModal1.activeSelf == true && Input.anyKeyDown) {
			instructionModal1.SetActive(false);
			instructionModal2.SetActive(true);
			return;
		}
		if (instructionModal2.activeSelf == true && Input.anyKeyDown) {
			loadingText.text = "Loading...";
			SceneManager.LoadScene(1);
		}
	}
	
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	
	public GameObject optionsModal;
	public GameObject volumeSlider;
	
	void Start() {
		Slider slider = volumeSlider.GetComponent<Slider>();
		slider.value = AudioListener.volume;
	}
	
	public void StartButtonPress() {
		Debug.Log("START");
		SceneManager.LoadScene(1);
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
	
}

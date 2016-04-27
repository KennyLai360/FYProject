using UnityEngine;
using System.Collections;

  public class SingletonController : MonoBehaviour {
       
	   public static SingletonController Instance;
	   
	   private AudioSource au;
	   
	   void Start() {
		   au = GetComponent<AudioSource>();
	   }
 
       void Awake() {
             if (Instance) {
			 DestroyImmediate(gameObject); }
             else
             {
                 DontDestroyOnLoad(gameObject);
                 Instance = this;
             }
       }
	   
	   void Update() {
		   //For generic and ease of remove the high score. J+K key together.
		   if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.K)) {
			   PlayerPrefs.DeleteAll();
			   Debug.Log("Player Prefs Data Deleted!");
			   }
	   }
	   
	   public void StopMusic() {
		   au.Stop();
	   }
	   
	   public void PauseMusic() {
		   au.Pause();
	   }
	   
	   public void PlayMusic() {
		   au.Play();
	   }
	   
	   public void ChangeAndPlayMusic(AudioClip newMusic) {
		   au.clip = newMusic;
		   PlayMusic();
	   }
	   
	   public void SetLoopProperty(bool value) {
			au.loop = value;
	   }
	   
 }

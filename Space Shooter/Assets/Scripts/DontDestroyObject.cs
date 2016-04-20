using UnityEngine;
using System.Collections;

  public class DontDestroyObject : MonoBehaviour
 {
       public static DontDestroyObject Instance;
 
       void Awake()
       {
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
			   Debug.Log("High scores deleted");
			   }
	   }
 }

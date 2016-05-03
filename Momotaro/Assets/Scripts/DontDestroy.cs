using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour 

{

public GameObject GO;

	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad(this.transform); 
		SceneManager.MoveGameObjectToScene(GO, (SceneManager.GetSceneByName("Menu")));
	}

}

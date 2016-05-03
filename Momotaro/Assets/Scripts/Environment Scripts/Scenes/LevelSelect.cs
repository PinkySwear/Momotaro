using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void BeginGame() {
	SceneManager.LoadScene(1);
	}

	// Update is called once per frame
	void Update () {
	}
}

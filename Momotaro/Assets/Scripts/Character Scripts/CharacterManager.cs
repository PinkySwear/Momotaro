﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour {

	public GameObject momoObject;
	public GameObject dogObject;
	public GameObject monkeyObject;
	public GameObject pheasantObject;
	
	public bool beginScene;

	public MomotaroBehavior momo;
	public DogFollow dog;
	public MonkeyFollow monkey;
	public PheasantFollow pheasant;

	public GameObject[] characterObjects;

	public bool holdSwitching;

	public GameObject[] topLevel;
	public GameObject[] withPheasant;
	public GameObject[] withMonkey;
	public GameObject[] withDog;


	// Use this for initialization
	void Start () {
		momo = momoObject.GetComponent<MomotaroBehavior> ();
		momo.controlling = true;
		dog = dogObject.GetComponent<DogFollow> ();
		dog.controlling = false;
		monkey = monkeyObject.GetComponent<MonkeyFollow> ();
		monkey.controlling = false;
		pheasant = pheasantObject.GetComponent<PheasantFollow> ();
		pheasant.controlling = false;
		holdSwitching = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (holdSwitching) {
			characterSwitchHold ();
		}
		else {
			characterSwitchPress ();
		}
		topLevel [0].SetActive (momo.controlling);
		topLevel [1].SetActive (dog.controlling);
		topLevel [2].SetActive (monkey.controlling);
		topLevel [3].SetActive (pheasant.controlling);
		foreach (GameObject g in withPheasant) {
			g.SetActive (pheasantObject.activeInHierarchy);
		}
		foreach (GameObject g in withMonkey) {
			g.SetActive (monkeyObject.activeInHierarchy);
		}
		foreach (GameObject g in withDog) {
			g.SetActive (dogObject.activeInHierarchy);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	void Awake() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 120;
	}

//	void OnGUI () {
//		GUI.color = Color.black;
//		if (holdSwitching) {
//			GUI.Label (new Rect (500, 10, 400, 20), "SWITCH BY HOLDING");
//			GUI.Label (new Rect (500, 40, 400, 20), "SWITCH SWITCHING MODE WITH \"O\"");
//
//		}
//		else {
//			GUI.Label (new Rect (500, 10, 400, 20), "SWITCH BY PRESSING");
//			GUI.Label (new Rect (500, 40, 400, 20), "SWITCH SWITCHING MODE WITH \"O\"");
//
//		}
//	
//	}

	void characterSwitchHold () {
		Debug.Log(beginScene);
		if ((Input.GetKey (KeyCode.D) || beginScene) && dogObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = true;
			dog.inParty = false;
			monkey.controlling = false;
			pheasant.controlling = false;

		}
		else if (Input.GetKey (KeyCode.S) && monkeyObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = true;
			monkey.inParty = false;
			pheasant.controlling = false;

		}
		else if (Input.GetKey (KeyCode.A) && pheasantObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = true;
			pheasant.inParty = false;

		}
		else {
			momo.controlling = true;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			holdSwitching = false;
		}
	}

	void characterSwitchPress () {
		if (Input.GetKeyDown (KeyCode.D)) {
			momo.controlling = true;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.S) && dogObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = true;
			dog.inParty = false;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.A) && monkeyObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = true;
			monkey.inParty = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.W) && pheasantObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = true;
			pheasant.inParty = false;
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			holdSwitching = true;
		}
	}
}

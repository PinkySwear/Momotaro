using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class AllInstructions : MonoBehaviour {

	public string[] messages;
	public bool[] isthere;
	public int number;
	public GameObject box;
	public GameObject text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		bool first = isthere[0];
		for(int i = 1; i < isthere.Length; i++){
			first = first || isthere[i];
		}
		if(first){
			box.SetActive(true);
			text.GetComponent<Text>().text = messages[number];
		}
		else{
			box.SetActive(false);
			text.GetComponent<Text>().text = "";
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Instructions : MonoBehaviour {

	public int num;
	public string message;
	public GameObject instruct; 
	public GameObject momo;

	// Use this for initialization
	void Start () {
		instruct.GetComponent<AllInstructions> ().messages[num] = message;
	}
	
	// Update is called once per frame
	void Update () {
		if((momo.GetComponent<Transform>().position.x <= transform.position.x) &&
		   (momo.GetComponent<Transform>().position.x + 10 >= transform.position.x)){
			instruct.GetComponent<AllInstructions> ().isthere[num] = true;
			instruct.GetComponent<AllInstructions> ().number = num;
			
		}
		else{
			instruct.GetComponent<AllInstructions> ().isthere[num] = false;
		}
	}
}

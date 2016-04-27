﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Level2Manager : MonoBehaviour {


	public GameObject momo;
	public GameObject dog;
	public GameObject monkey;
	public GameObject textbox;
	
	public string[] messages;
	public GameObject[] bubbles;
	public GameObject[] baddies;
	public GameObject[] door;
	
	public int count;
	public int scene;
	private float startTime;

	// Use this for initialization
	void Start () {
		// Scene 1
		messages[0] = "Momo, do you hear that?";
		messages[1] = "Sounds like somebody in trouble! ";
		messages[2] = "I smell Oni, Momo--lots of them.";
		messages[3] = "It doesn’t matter how many of them there are! We’ve got to help whoever’s in trouble!";
		// Scene 2
		messages[4] = "Are you okay?";
		//MONKEY: 👌! 🙏🏼 😊. 
		messages[5] = "O… kay. You… don’t talk?";
		//MONKEY: 🙊
		messages[6] = "Got it.";
		
		// Scene 3
		//MONKEY: 🐩!
		messages[7] = "Er… yes?"; //dog
		//MONKEY: 🍌, 👇.
		messages[8] = "You… want a banana?"; //dog
		//MONKEY: ! // Oni appears

		// Scene 4
		//messages[] Don’t worry, he’s not coming with us anyway. Goodbye, little fella!

		bubbles[0].SetActive(false);
		bubbles[1].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		// Scene 1
		
		if(momo.GetComponent<Transform> ().position.x >= 127f){
			scene = 1;
		}
		if(scene == 1 && count < 4){
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			if((count % 2) ==0){
				bubbles[0].SetActive(true);
				bubbles[1].SetActive(false);
			}
			else{
				bubbles[1].SetActive(true);
				bubbles[0].SetActive(false);
			}
			textbox.GetComponent<Text>().text = messages[count];
			if(Input.GetKey(KeyCode.Return) &&((Time.time-startTime) > 0.5f)){
				startTime = Time.time;
				count ++;
			}
		}
		if(count >= 4 && scene == 1){
			momo.GetComponent<MomotaroBehavior> ().stop = false;
			bubbles[1].SetActive(false);
			bubbles[0].SetActive(false);
			textbox.GetComponent<Text>().text = "";
		}
		
		// Scene 2
		if(dog.GetComponent<Transform> ().position.x >= 142f && momo.GetComponent<Transform> ().position.x >= 142f){
			scene = 2;
		}
		if(scene == 2){
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			if(door[0].GetComponent<Transform> ().position.y >= -21.5f && (Time.time-startTime) > 0.1f){
				startTime = Time.time;
				door[0].GetComponent<Transform> ().position = new Vector3(door[0].GetComponent<Transform> ().position.x,door[0].GetComponent<Transform> ().position.y -0.5f,door[0].GetComponent<Transform> ().position.z);
			}
			else if(door[0].GetComponent<Transform> ().position.y <= -21.5f){
				for(int i = 0; i < 6; i++){
					if(baddies[i] != null){
						baddies[i].SetActive(true);
					}
				}
				scene = 3;
				momo.GetComponent<MomotaroBehavior> ().stop = false;
			}
		}
		if(scene == 3){
			bool canProgress = true;
			for(int i = 0; i < 6; i++){
				canProgress = canProgress && (baddies[i] == null);
			}
			if(canProgress){
				scene = 4;
			}
		}
		if(scene == 4){
			if(door[1].GetComponent<Transform> ().position.y <= -17f && (Time.time-startTime) > 0.1f){
				startTime = Time.time;
				door[1].GetComponent<Transform> ().position = new Vector3(door[1].GetComponent<Transform> ().position.x,door[1].GetComponent<Transform> ().position.y +0.5f,door[1].GetComponent<Transform> ().position.z);
			}
			else if(door[1].GetComponent<Transform> ().position.y >= -17f){
			}
		}
		
		// Scene 3
		
		if(dog.GetComponent<Transform> ().position.x >= 160f && momo.GetComponent<Transform> ().position.x >= 160f){
			scene = 5;
		}
		
		if(scene == 5){
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			if(door[2].GetComponent<Transform> ().position.y >= -21.5f && (Time.time-startTime) > 0.1f){
				startTime = Time.time;
				door[2].GetComponent<Transform> ().position = new Vector3(door[2].GetComponent<Transform> ().position.x,door[2].GetComponent<Transform> ().position.y -0.5f,door[2].GetComponent<Transform> ().position.z);
			}
			else if(door[2].GetComponent<Transform> ().position.y <= -21.5f){
				for(int i = 6; i < 13; i++){
					if(baddies[i] != null){
						baddies[i].SetActive(true);
					}
				}
				scene = 6;
				momo.GetComponent<MomotaroBehavior> ().stop = false;
			}
		}
		if(scene == 6){
			bool canProgress = true;
			for(int i = 6; i < 13; i++){
				canProgress = canProgress && (baddies[i] == null);
			}
			if(canProgress){
				scene = 7;
			}
			else{
				door[4].GetComponent<Transform> ().position = new Vector3(door[4].GetComponent<Transform> ().position.x,door[4].GetComponent<Transform> ().position.y -0.001f,door[4].GetComponent<Transform> ().position.z);
				if(door[4].GetComponent<Transform> ().position.y <= -19f){
					scene = 69;
					textbox.GetComponent<Text>().text = "Game Over!";
				}
			}
		}
		if(scene == 7){
			if(door[3].GetComponent<Transform> ().position.y <= -17f && (Time.time-startTime) > 0.1f){
				startTime = Time.time;
				door[3].GetComponent<Transform> ().position = new Vector3(door[3].GetComponent<Transform> ().position.x,door[3].GetComponent<Transform> ().position.y +0.5f,door[3].GetComponent<Transform> ().position.z);
			}
			else if(door[3].GetComponent<Transform> ().position.y >= -17f){
				textbox.GetComponent<Text>().text = "Press <Enter> to get the Monkey out of the cage";
				if(Input.GetKey(KeyCode.Return)){
					scene = 8;
					monkey.SetActive(true);
				}
			}
		}
		if(scene == 8){
			
		}
	}
}

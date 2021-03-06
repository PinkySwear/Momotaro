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
	public GameObject cam;
	public GameObject caged;
	
	public AudioSource[] songs;
	
	public string[] messages;
	public GameObject[] bubbles;
	public GameObject[] baddies;
	public GameObject[] door;
	public int moveOn;
	
	public int count;
	public int scene;
	private float startTime;
	public float counter = 75f;

	// Use this for initialization
	void Start () {
		songs = gameObject.GetComponents<AudioSource>();
		songs[0].volume = 0f;
		songs[1].volume = 0f;
		// Scene 1
		messages[0] = "Momo, do you hear that?";
		messages[1] = "Sounds like somebody in trouble! ";
		messages[2] = "I smell Oni, Momo--lots of them.";
		messages[3] = "It doesn’t matter how many of them there are! We’ve got to help whoever’s in trouble!";

		messages[4] = "Momo, look, a Monkey!";
		messages[5] = "Hey there little guy! We got here just in time! ";
		messages[6] = "Maybe he wants to come with us?";
		messages[7] = "Well...I suppose....he might be useful, I guess.";

		messages[8] = "Wow, that was a lot of Oni!";
		messages[9] = "It means we're getting closer to where they're coming from!";
		messages[10] = "Yes! We need to move quickly, though, lest someone else gets hurt!";
		messages[11] = "Agreed!  Come along guys, we have work to do!";
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
		
		if(scene == 5 && moveOn < 1){
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			if(door[2].GetComponent<Transform> ().position.y >= -21.5f && (Time.time-startTime) > 0.1f){
				startTime = Time.time;
				cam.GetComponent<AudioSource> ().volume -= 0.1f;
				door[2].GetComponent<Transform> ().position = new Vector3(door[2].GetComponent<Transform> ().position.x,door[2].GetComponent<Transform> ().position.y -0.5f,door[2].GetComponent<Transform> ().position.z);
			}
			else if(door[2].GetComponent<Transform> ().position.y <= -21.5f){
				for(int i = 6; i < 13; i++){
					if(baddies[i] != null){
						baddies[i].SetActive(true);
					}
				}
				scene = 6;
				songs[0].volume = 1f;
				momo.GetComponent<MomotaroBehavior> ().stop = false;
			}
		}
		if(scene == 6 && moveOn < 1){
			bool canProgress = true;
			for(int i = 6; i < 13; i++){
				canProgress = canProgress && (baddies[i] == null);
			}
			if(canProgress){
				moveOn = 1;
			}
			else{
				if((Time.time - startTime) >= 0.1f){
					startTime = Time.time;
					counter -= 0.1f;
					door[4].GetComponent<Transform> ().position = new Vector3(door[4].GetComponent<Transform> ().position.x,door[4].GetComponent<Transform> ().position.y -0.01f,door[4].GetComponent<Transform> ().position.z);
				}
				if(door[4].GetComponent<Transform> ().position.y <= -19.5f){
					scene = 69;
					textbox.GetComponent<Text>().text = "Game Over!";
				}
				else{
				//	textbox.GetComponent<Text>().text = Mathf.Round(counter) +" seconds";
				}
			}
		}
		Debug.Log(songs[0].volume);
		if(moveOn == 1){
			//Debug.Log("Done!");
			if(door[3].GetComponent<Transform> ().position.y <= -17f && (Time.time-startTime) > 0.1f){
				startTime = Time.time;
				songs[0].volume -= 0.1f;
				door[3].GetComponent<Transform> ().position = new Vector3(door[3].GetComponent<Transform> ().position.x,door[3].GetComponent<Transform> ().position.y +0.1f,door[3].GetComponent<Transform> ().position.z);
			}
			else if(door[3].GetComponent<Transform> ().position.y >= -17f){
				moveOn = 2;
			}
		}
		else if(moveOn == 2){
			cam.GetComponent<AudioSource> ().volume = 0f;
			songs[0].volume = 0f;
			songs[1].volume = 1f;
			caged.SetActive(false);
			monkey.SetActive(true);
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			Debug.Log("Test!");
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
			if(count > 7){
				moveOn = 3;
				momo.GetComponent<MomotaroBehavior> ().stop = false;
				songs[1].volume = 0f;
				cam.GetComponent<AudioSource> ().volume = 1f;
			}
		}
		else if(moveOn == 3){
			if(dog.GetComponent<Transform> ().position.x >= 185f && momo.GetComponent<Transform> ().position.x >= 185f && monkey.GetComponent<Transform> ().position.x >= 185f){
				moveOn = 4;
			}
		}
		
		else if(moveOn == 4){
			if(door[5].GetComponent<Transform> ().position.y >= -24f && (Time.time-startTime) > 0.1f){
				startTime = Time.time;
				door[5].GetComponent<Transform> ().position = new Vector3(door[5].GetComponent<Transform> ().position.x,door[5].GetComponent<Transform> ().position.y -0.5f,door[5].GetComponent<Transform> ().position.z);
			}
			else if(door[5].GetComponent<Transform> ().position.y <= -24f){
				for(int i = 13; i < 20; i++){
					if(baddies[i] != null){
						baddies[i].SetActive(true);
					}
				}
				moveOn = 5;
			}
		}
		else if(moveOn == 5){
			bool canProgress = true;
			for(int i = 13; i < 20; i++){
				canProgress = canProgress && (baddies[i] == null);
			}
			if(canProgress){
				if(door[6].GetComponent<Transform> ().position.y <= -19f && (Time.time-startTime) > 0.1f){
					startTime = Time.time;
					door[6].GetComponent<Transform> ().position = new Vector3(door[6].GetComponent<Transform> ().position.x,door[6].GetComponent<Transform> ().position.y +0.1f,door[6].GetComponent<Transform> ().position.z);
				}
				else if(door[6].GetComponent<Transform> ().position.y >= -19f){
					moveOn = 6;
				}
			}
		}
		else if(moveOn == 6){
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			Debug.Log("Test!");
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
			if(count > 11){
				moveOn = 7;
				momo.GetComponent<MomotaroBehavior> ().stop = false;
				songs[1].volume = 0f;
				cam.GetComponent<AudioSource> ().volume = 1f;
			}
		}
		else if(moveOn ==7){
			if(momo.GetComponent<Transform>().position.x > 210f){
				textbox.GetComponent<Text>().text = "Press 'Enter' to progress to next level!";
				if(Input.GetKey (KeyCode.Return)){
					SceneManager.LoadScene ("FinalLevel3");
				}
			}
		}
	}
}

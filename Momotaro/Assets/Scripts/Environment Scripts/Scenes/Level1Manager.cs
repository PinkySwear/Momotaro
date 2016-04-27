using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour {


	public GameObject[] onis;
	public GameObject[] goonSquad;
	public GameObject pillaroni;
	public GameObject dog;
	public GameObject cam;
	public GameObject momo;
	public GameObject companion;
	public GameObject textbox;
	public int scene;
	public bool[] onisKilled;
	public AudioSource[] companionSong;
	//public AudioSource fightSong;
	
	private bool move1;
	private bool slowdown;
	public int beginFight;
	private float startTime;
	private float beginTime;

	// Use this for initialization
	void Start () {
		companionSong = gameObject.GetComponents<AudioSource>();
		companionSong[0].volume = 0f;
		companionSong[1].volume = 0f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(scene ==0){
			move1 = true;
			for(int i=0; i<onis.Length ;i++){
				onisKilled[i] = onis[i].GetComponent<DerpBehavior> ().isDead;
				move1 = onisKilled[i] && move1;
			}
		}
		if(scene == 2){
			//Debug.Log(momo.GetComponent<Transform> ().position.x);
			if(momo.GetComponent<Transform> ().position.x >= 88f &&
			   dog.GetComponent<Transform> ().position.x < 90f){
				dog.GetComponent<LevelOneDialogue> ().moveDog = true;
			}
			else if(dog.GetComponent<Transform> ().position.x >= 90f){
				dog.GetComponent<LevelOneDialogue> ().moveDog = false;
				scene ++;
				dog.GetComponent<LevelOneDialogue> ().scene = scene;
			}
			if(momo.GetComponent<Transform> ().position.x >= 93f){
				momo.GetComponent<MomotaroBehavior> ().stop = true;
			}
		}
		
		if(scene == 4){
			//Debug.Log(momo.GetComponent<Transform> ().position.x);
			if(momo.GetComponent<Transform> ().position.x >= 103f &&
			   dog.GetComponent<Transform> ().position.x < 98f){
				dog.GetComponent<LevelOneDialogue> ().moveDog = true;
				momo.GetComponent<MomotaroBehavior> ().stop = true;
				dog.GetComponent<LevelOneDialogue> ().scene = 5;
			}
			else if(dog.GetComponent<Transform> ().position.x >= 98f){
				dog.GetComponent<LevelOneDialogue> ().moveDog = false;
				scene ++;
			}
		}
		if(dog.GetComponent<LevelOneDialogue> ().scene == 6){
			scene = 6;
			if(pillaroni.GetComponent<Transform> ().position.y < -30){
				scene++;
				textbox.GetComponent<Text>().text = "";
				beginTime = Time.time;
			}
			else{
				if(momo.GetComponent<MomotaroBehavior> ().controlling){
					textbox.GetComponent<Text>().text = "Press 'S' to switch to control Dog";
				}
				else{
					textbox.GetComponent<Text>().text = "You may press 'D' to switch back, press 'Space' to bark at the oni.";
				}
			}
		}
		if(scene == 7){
			companionSong[0].volume = 0f;
			if(momo.GetComponent<Transform> ().position.x <= 108f){
				textbox.GetComponent<Text>().text = "Ugh, I guess you can come with me...";
			}
			else{
				textbox.GetComponent<Text>().text = "";
				scene ++;
			}
		}
		
		if(scene == 8 && momo.GetComponent<Transform> ().position.x < 140f){
			if(companion.GetComponent<Transform> ().position.x >= 139f &&
			   companion.GetComponent<Transform> ().position.y >= -19f){
				textbox.GetComponent<Text>().text = "Good! Press 'Enter' to Let Momo through!";
				if(Input.GetKey (KeyCode.Return)){
					textbox.GetComponent<Text>().text = "";
					Debug.Log(scene);
					scene = 9;
					momo.GetComponent<Transform> ().position = new Vector3 (140f,-18f,0f);
					Debug.Log(scene);
				}
			}
			else if(momo.GetComponent<Transform> ().position.x >= 133f && momo.GetComponent<Transform> ().position.x < 140f){
				textbox.GetComponent<Text>().text = "You may press the 'Down Arrow' on colored dirt to dig with dog.";
			}
		}
		else if(scene ==8 && momo.GetComponent<Transform> ().position.x < 154f && beginFight==0){
			if(companion.GetComponent<Transform> ().position.x >= 154f &&
			   companion.GetComponent<Transform> ().position.y >= -19f){
				textbox.GetComponent<Text>().text = "Press 'Enter' to Let Momo through!";
				if(Input.GetKey (KeyCode.Return)){
					textbox.GetComponent<Text>().text = "";
					scene = 10;
					momo.GetComponent<Transform> ().position = new Vector3 (154f,-15f,0f);
					scene = 10;
					beginFight = 1;
				}
			}
			else{
				textbox.GetComponent<Text>().text = "";
			}
		}
		else if(beginFight == 1 && scene ==8){
			textbox.GetComponent<Text>().text = "Wait...this doesn't seem right... <Press R>";
			if(Input.GetKey (KeyCode.R)){
				textbox.GetComponent<Text>().text = "Oh no! It's an ambush!";
				companionSong[1].volume = 1f;
				scene = 11;
				for (int i =0; i < goonSquad.Length; i++){
					goonSquad[i].SetActive(true);
				}
				beginFight = 2;
			}
			cam.GetComponent<AudioSource> ().volume = 0f;
		}
		if(momo.GetComponent<Transform> ().position.x > 190f){
			textbox.GetComponent<Text>().text = "Congrats! Press X to progress to the next level!";
			if(Input.GetKey (KeyCode.X)){
				SceneManager.LoadScene ("AlphaDemo");
			}
		}
		
		if(scene == 9){
			if(companion.GetComponent<Transform> ().position.x >= 146f &&
			   companion.GetComponent<Transform> ().position.y >= -16f){
				textbox.GetComponent<Text>().text = "Press 'Enter' to Let Momo through!";
				if(Input.GetKey (KeyCode.Return)){
					textbox.GetComponent<Text>().text = "";
					scene = 10;
					momo.GetComponent<Transform> ().position = new Vector3 (154f,-15f,0f);
				}
			}
			else{
				textbox.GetComponent<Text>().text = "";
			}
		}
		
		if(scene == 10){
			textbox.GetComponent<Text>().text = "Wait...this doesn't seem right... <Press Enter>";
			if(Input.GetKey (KeyCode.Return)){
				textbox.GetComponent<Text>().text = "Oh no! It's an ambush!";
				companionSong[1].volume = 1f;
				scene = 11;
			}
			cam.GetComponent<AudioSource> ().volume = 0f;
		}
		
		if(move1){
			slowdown = true;
			scene ++;
			move1 = false;
			dog.GetComponent<LevelOneDialogue> ().scene = scene;
		}
		if(scene ==3 && cam.GetComponent<AudioSource> ().volume >= 0.01){
			slowdown = true;
		}
		if(slowdown && ((Time.time - startTime) > 0.1f) && dog.GetComponent<LevelOneDialogue> ().seesMomo){
			//Debug.Log("wowza");
			startTime = Time.time;
			cam.GetComponent<AudioSource> ().volume -= 0.1f;
			if(cam.GetComponent<AudioSource> ().volume <= 0.01){
				slowdown = false;
			}
		}
		//Debug.Log(dog.GetComponent<LevelOneDialogue> ().scene);
		//Debug.Log(dog.GetComponent<LevelOneDialogue> ().scene);
		if((dog.GetComponent<LevelOneDialogue> ().count >= 2) && ((Time.time - startTime) > 0.1f) &&
		   (dog.GetComponent<LevelOneDialogue> ().scene % 2 == 1) && (scene < 4)){
			//Debug.Log("wowie");
			startTime = Time.time;
			companionSong[0].volume += 0.1f;
		}
		else if((dog.GetComponent<LevelOneDialogue> ().scene % 2 == 0) && ((Time.time - startTime) > 0.1f) &&
				cam.GetComponent<AudioSource> ().volume < 0.99f && scene < 5){
				//Debug.Log("bazinga");
				startTime = Time.time;
				cam.GetComponent<AudioSource> ().volume += 0.1f;
				companionSong[0].volume -= 0.1f;
				if(dog.GetComponent<LevelOneDialogue> ().scene ==2){
					scene = 2;
				}
				else{
					scene = 4;
				}
		}
		
		
	}
}

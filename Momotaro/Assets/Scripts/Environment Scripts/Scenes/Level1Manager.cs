using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour {


	public GameObject[] onis;
	public GameObject dog;
	public GameObject cam;
	public GameObject momo;
	public int scene;
	public bool[] onisKilled;
	public AudioSource companionSong;
	
	private bool move1;
	private bool slowdown;
	private float startTime;

	// Use this for initialization
	void Start () {
		companionSong = gameObject.GetComponent<AudioSource>();
		companionSong.volume = 0f;
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
			Debug.Log("wowza");
			startTime = Time.time;
			cam.GetComponent<AudioSource> ().volume -= 0.1f;
			if(cam.GetComponent<AudioSource> ().volume <= 0.01){
				slowdown = false;
			}
		}
		//Debug.Log(dog.GetComponent<LevelOneDialogue> ().scene);
		//Debug.Log(dog.GetComponent<LevelOneDialogue> ().scene);
		if((dog.GetComponent<LevelOneDialogue> ().count >= 2) && ((Time.time - startTime) > 0.1f) &&
		   (dog.GetComponent<LevelOneDialogue> ().scene % 2 == 1)){
			Debug.Log("wowie");
			startTime = Time.time;
			companionSong.volume += 0.1f;
		}
		else if((dog.GetComponent<LevelOneDialogue> ().scene % 2 == 0) && ((Time.time - startTime) > 0.1f) &&
				cam.GetComponent<AudioSource> ().volume < 0.99f){
				Debug.Log("bazinga");
				startTime = Time.time;
				cam.GetComponent<AudioSource> ().volume += 0.1f;
				companionSong.volume -= 0.1f;
				if(dog.GetComponent<LevelOneDialogue> ().scene ==2){
					scene = 2;
				}
				else{
					scene = 4;
				}
		}
		
		
	}
}

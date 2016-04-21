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
		Debug.Log(scene);
		if(scene == 2){
			Debug.Log(momo.GetComponent<Transform> ().position.x);
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
		
		
		if(move1){
			slowdown = true;
			scene ++;
			move1 = false;
			dog.GetComponent<LevelOneDialogue> ().scene = scene;
		}
		if(slowdown && ((Time.time - startTime) > 0.1f) && dog.GetComponent<LevelOneDialogue> ().seesMomo){
			startTime = Time.time;
			cam.GetComponent<AudioSource> ().volume -= 0.1f;
			if(cam.GetComponent<AudioSource> ().volume == 0){
				slowdown = false;
			}
		}
		if((dog.GetComponent<LevelOneDialogue> ().count >= 2) && ((Time.time - startTime) > 0.1f) &&
		   (dog.GetComponent<LevelOneDialogue> ().scene == 1)){
			//Debug.Log("wowie");
			startTime = Time.time;
			companionSong.volume += 0.1f;
		}
		else if((dog.GetComponent<LevelOneDialogue> ().scene == 2) && ((Time.time - startTime) > 0.1f) &&
				cam.GetComponent<AudioSource> ().volume < 0.99f){
				startTime = Time.time;
				cam.GetComponent<AudioSource> ().volume += 0.1f;
				companionSong.volume -= 0.1f;
				scene = 2;
		}
		
		
	}
}

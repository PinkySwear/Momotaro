using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Level4Manager : MonoBehaviour {

	public GameObject momo;
	public GameObject dog;
	public GameObject monkey;
	public GameObject nest;
	public GameObject bird;
	public GameObject textbox;
	public GameObject lift;
	public GameObject cam;
	public AudioSource[] songs;
	
	public float incr;
	public string[] messages;
	public GameObject[] bubbles;
	public int count;
	
	public bool isOn;
	
	// 198, -48

	// Use this for initialization
	void Start () {
		messages[0] = "Hey momo, look at the lift. I wonder if Monkey could climb up there and tun it on...";
		songs = gameObject.GetComponents<AudioSource>();
		songs[0].volume = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("wow");
		if(momo.GetComponent<Transform> ().position.x >= 10f && !isOn){
			bubbles[0].SetActive(true);
			textbox.GetComponent<Text>().text = messages[count];
		}
		if(monkey.GetComponent<Transform> ().position.x >= 25f && monkey.GetComponent<Transform> ().position.y >= -108f && !isOn){
			bubbles[0].SetActive(false);
			textbox.GetComponent<Text>().text = "Press 'Enter' to turn on the lift";
			if(Input.GetKey (KeyCode.Return)){
				isOn = true;
				incr = 0.05f;
			}
		}
		if(isOn){
			if(lift.GetComponent<Transform>().position.y >= 10f){
				incr = -0.05f;
			}
			if(lift.GetComponent<Transform>().position.y <= -3f){
				incr = 0.05f;
			}
			lift.GetComponent<Transform>().position = new Vector3 (lift.GetComponent<Transform>().position.x,lift.GetComponent<Transform>().position.y+incr,lift.GetComponent<Transform>().position.z);
			if(momo.GetComponent<Transform> ().position.x >= 199f && monkey.GetComponent<Transform> ().position.x >= 199f && dog.GetComponent<Transform> ().position.x >= 199f){
				nest.SetActive(false);
				bird.SetActive(true);
				cam.GetComponent<AudioSource> ().volume -= 0.01f;
				songs[0].volume += 0.01f;
			}
		}
		if(Input.GetKey (KeyCode.Q)){
			momo.GetComponent<Transform> ().position = new Vector3 (198f,-48f,0f);
			dog.GetComponent<Transform> ().position = new Vector3 (198f,-48f,0f);
			monkey.GetComponent<Transform> ().position = new Vector3 (198f,-48f,0f);
			isOn = true;
		}
	}
}

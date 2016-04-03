using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioControl : MonoBehaviour {

	public GameObject momo;
	public GameObject dog;
	public GameObject bird;
	public GameObject monkey;
	public AudioSource[] aSources;
	
	private bool pressed;

	// Use this for initialization
	void Start () {
		aSources = gameObject.GetComponents<AudioSource>();
		aSources[0].volume = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(momo.GetComponent<MomotaroBehavior>().controlling){
			if(momo.GetComponent<MomotaroBehavior>().movingLeft || momo.GetComponent<MomotaroBehavior>().movingRight){
				aSources[0].volume = 0.25f;
			}
			else{
				aSources[0].volume = 0f;
			}
			if(Input.GetKey(KeyCode.UpArrow) && !pressed){
				aSources[1].Play();
				pressed = true;
			}
			if(!aSources[1].isPlaying){
				//aSources[1].Stop();
				pressed = false;
			}
		}
	}
}

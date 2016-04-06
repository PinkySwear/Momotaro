using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioControl : MonoBehaviour {

	public GameObject momo;
	public GameObject dog;
	public GameObject bird;
	public GameObject monkey;
	public GameObject[] switches;
	public AudioSource[] aSources;
	public bool[] actives;
	
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
		for (int i = 0; i < switches.Length; i++){
			if((switches[i].GetComponent<InteractionControl>().activated) &&
			   (!actives[i])){
				actives[i] = true;
				aSources[2].Play();
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelOneDialogue : MonoBehaviour {

	public bool hasDefeatOni;

	public GameObject momo;
	public GameObject text;
	public GameObject box;
	public GameObject[] dogDiag;
	public GameObject[] momoDiag;
	public AudioSource[] voices;
	public AudioSource[] music;
	
	private bool seesMomo;
	private int count;
	private int dogIdx;
	private int momoIdx;
	private int diagLimit = 20;
	private bool stop;
	private bool isMomo;
	private int moveDog;
	private float refractTime;
	private float refract = 0.4f;
	private float startTime;
	private float pause = 1f;
	private int wordCount;
	
	// Use this for initialization
	void Start () {
		voices = gameObject.GetComponents<AudioSource>();
		startTime = Time.time;
		refractTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(!stop){
			if((momo.GetComponent<Transform>().position.x <= transform.position.x) &&
			   (momo.GetComponent<Transform>().position.x + 2 >= transform.position.x)){
				seesMomo = true;
			}
			
			if(hasDefeatOni && seesMomo){
				stop = true;
			}
		}
		else{
			if(Input.GetKey(KeyCode.Return) && 
			   ((Time.time-startTime) >= pause) &&
			   (count != 15)){
				count ++;
				wordCount = 0;
				startTime = Time.time;
				refractTime = Time.time;
				if(count == 1){
					dogIdx ++;
					isMomo = false;
				}
				else if(count != 15){
					momoIdx ++;
					isMomo = true;
				}
			}
			else if(Input.GetKey(KeyCode.Return) &&
			        ((Time.time-startTime) >= pause) &&
					count == 15){
				stop = false;
			}
			for(int i = 0; i < dogDiag.Length; i++){
				if(dogIdx == i && !isMomo){
					dogDiag[dogIdx].SetActive(true);
				}
				else{
					dogDiag[i].SetActive(false);
				}
			}
			for(int i = 0; i < momoDiag.Length; i++){
				if(momoIdx == i && isMomo){
					momoDiag[momoIdx].SetActive(true);
				}
				else{
					momoDiag[i].SetActive(false);
				}
			}
			if((Time.time-refractTime) >= refractTime){
				if(isMomo && wordCount < 3){
					voices[0].Play();
					wordCount ++;
					refractTime = Time.time;
				}
				else if(wordCount < 3){
					voices[1].Play();
					wordCount ++;
					refractTime = Time.time;
				}
			}
		}
	}
	
	void FixedUpdate () {
		
	}
}


﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelOneDialogue : MonoBehaviour {

	public bool hasDefeatOni;
	public float velocity;
	public float jumpForce;
	public bool jump = false;

	public GameObject dogs;
	public GameObject momo;
	public GameObject text;
	public GameObject box;
	public GameObject[] dogDiag;
	public GameObject[] momoDiag;
	public AudioSource[] voices;
	public AudioSource[] music;
	
	private bool onSomething;
	public bool moveDog;
	private Rigidbody dogRb;
	private CapsuleCollider myCollider;
	private bool isDone;
	
	private bool seesMomo;
	private int count;
	private int dogIdx = -1;
	private int momoIdx = -1;
	private int diagLimit = 20;
	public bool stop;
	private bool isMomo;
	private float refractTime;
	private float refract = 0.15f;
	private float startTime;
	private float pause = 0.5f;
	private float jumpTime;
	private int wordCount;
	
	// Use this for initialization
	void Start () {
		voices = gameObject.GetComponents<AudioSource>();
		startTime = Time.time;
		refractTime = Time.time;
		jumpTime = Time.time;
		dogRb = GetComponent<Rigidbody> ();
		//myCollider = gameObject.GetComponent<CapsuleCollider> ();
		dogRb.freezeRotation = true;
		velocity = 10f;
		jumpForce = 1000f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.X)){
			SceneManager.LoadScene ("AlphaDemo");
		}
	}
	
	void FixedUpdate () {
		
		if (moveDog) {
			//restrict movement to one plane
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			dogRb.velocity = new Vector3 (velocity, dogRb.velocity.y, dogRb.velocity.z);
			Vector3 s = transform.localScale;
			s.x = 1;
			transform.localScale = s;
			if((transform.position.x >= 131f && count < 20) || 
			   (transform.position.x >= 179f)){
				moveDog = false;
				if(count < 20){
					count = 17;
					dogs.GetComponent<Transform> ().position = new Vector3 (30f,transform.position.y,transform.position.z);
				}
				else{
					Debug.Log("yo");
					count = 23;
					dogs.GetComponent<Transform> ().position = new Vector3 (75f,transform.position.y,transform.position.z);
				}
			}
			if((Time.time - jumpTime) >= 1f){
				Debug.Log("Jump!");
				transform.position = new Vector3 (transform.position.x, transform.position.y+1f, 0f);
				jumpTime = Time.time;
				//dogRb.AddForce (Vector3.up * jumpForce);
			}
		}
		velocity = 10f;
		
	}
}

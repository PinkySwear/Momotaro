using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;


public class Level0Manager : MonoBehaviour {

	public GameObject momo;
	public GameObject mom;
	public GameObject dad;
	public GameObject textbox;
	public GameObject chest;
	public GameObject[] bubbles;
	public GameObject wall;
	public string[] messages;
	
	public float velocity;
	public bool moveDad;
	public bool moveBoth;
	private Rigidbody dadRb;
	private Rigidbody momRb;
	
	private bool nearSword;
	private bool nearMom;
	private int swordScene;
	private int count;
	private float startTime;
	private int enterConvo;
	private bool isDone;
	private bool theEnd;
	//private inUse;

	// Use this for initialization
	void Start () {
		dadRb = dad.GetComponent<Rigidbody> ();
		dadRb.freezeRotation = true;
		momRb = mom.GetComponent<Rigidbody> ();
		momRb.freezeRotation = true;
		velocity = 10f;
		//jumpForce = 1000f;
		
		// Scene 1
		messages[0] = "How did you sleep, my little peach?";
		messages[1] = "I had that dream again, Mom. The one with those demons.";
		messages[2] = "You mean the Oni?";
		messages[3] = "Yeah. Are they all gone?";
		messages[4] = "Yes, Momotaro, they are. Ever since you came to us from the heavens, our little gift inside a big peach. ";
		messages[5] = "Mom, where did I come from?";
		messages[6] = "From a peach! You know that!";
		messages[7] = "Well, yeah, but… where did the peach come from?";
		// Scene 2
		messages[8] = "Honey!";
		messages[9] = "What's wrong?";
		messages[10] = "Oni! Sighted in the hills outside of town.";
		messages[11] = "Oni?! But they haven’t been seen around here since…";
		// Scene 4
		messages[12] = "Dad, what’s going on?!";
		messages[13] = "There’s no time to talk, everyone is already assembling in the village square to decide what to do. Let’s go.";
		messages[14] = "Can I come, Dad?";
		messages[15] = "Absolutely not. This is for adults. You stay here, and don’t leave the house.";
		// Sword Scene
		messages[16] = "Through the darkness of future's past...";
		messages[17] = "The magician longs to see.";
		messages[18] = "One chants out between two worlds...";
		messages[19] = "'Fire... walk with me.'";
		
		bubbles[0].SetActive(false);
		bubbles[1].SetActive(false);
		bubbles[2].SetActive(false);
		bubbles[3].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(momo.GetComponent<Transform> ().position.x <= 27f){
			textbox.GetComponent<Text>().text = "Press 'Enter' to Examine Dad's Sword";
			nearSword = true;
			nearMom = false;
		}
		else if(momo.GetComponent<Transform> ().position.x >= 34f){
			textbox.GetComponent<Text>().text = "Press 'Enter' to Talk with Mom";
			nearMom = true;
			nearSword = false;
		}
		else{
			textbox.GetComponent<Text>().text = "";
			nearMom = false;
			nearSword = false;
		}
		
		// Conversation Interaction
		if(nearMom && Input.GetKey(KeyCode.Return) && enterConvo==0){
			enterConvo = 1;
		}
		Debug.Log(enterConvo);
		if(enterConvo == 1){
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			if((count % 2) ==0){
				bubbles[2].SetActive(true);
				bubbles[1].SetActive(false);
			}
			else{
				bubbles[1].SetActive(true);
				bubbles[2].SetActive(false);
			}
			textbox.GetComponent<Text>().text = messages[count];
			if(Input.GetKey(KeyCode.Return) &&((Time.time-startTime) > 0.5f)){
				startTime = Time.time;
				count ++;
			}
			if(count > 7){
				enterConvo = 2;
				moveDad = true;
				Debug.Log(moveDad);
			}
		}
		else if(enterConvo == 2){
			bubbles[1].SetActive(false);
			bubbles[2].SetActive(false);
			bubbles[3].SetActive(false);
			if(!moveDad){
				enterConvo = 3;
			}
		}
		else if(enterConvo == 3){
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			mom.GetComponent<Transform> ().rotation = new Quaternion (0f, 180f, 0f, 0f);
			if((count % 2) ==0){
				bubbles[3].SetActive(true);
				bubbles[2].SetActive(false);
			}
			else{
				bubbles[2].SetActive(true);
				bubbles[3].SetActive(false);
			}
			textbox.GetComponent<Text>().text = messages[count];
			if(Input.GetKey(KeyCode.Return) &&((Time.time-startTime) > 0.5f)){
				startTime = Time.time;
				count ++;
			}
			if(count > 11){
				enterConvo = 4;
			}
		}
		else if(enterConvo == 4){
			if((count % 2) ==0){
				bubbles[1].SetActive(true);
				bubbles[2].SetActive(false);
				bubbles[3].SetActive(false);
			}
			else if(count == 13){
				bubbles[3].SetActive(true);
				bubbles[1].SetActive(false);
				bubbles[2].SetActive(false);
			}
			else if(count == 15){
				bubbles[3].SetActive(false);
				bubbles[1].SetActive(false);
				bubbles[2].SetActive(true);
			}
			textbox.GetComponent<Text>().text = messages[count];
			if(Input.GetKey(KeyCode.Return) &&((Time.time-startTime) > 0.5f)){
				startTime = Time.time;
				count ++;
			}
			if(count > 15){
				enterConvo = 5;
				momo.GetComponent<MomotaroBehavior> ().stop = false;
				bubbles[3].SetActive(false);
				bubbles[1].SetActive(false);
				bubbles[2].SetActive(false);
				moveBoth = true;
			}
		}
		
		// Sword Interaction
		if(nearSword && Input.GetKey(KeyCode.Return) && !isDone){
			swordScene = 1;
		}
		else if(nearSword && Input.GetKey(KeyCode.Return)){
			if(count < 20){
				swordScene = 2;
			}
		}
		if(nearSword && swordScene == 1){
			textbox.GetComponent<Text>().text = "Momotaro! Come eat your breakfast!";
			bubbles[0].SetActive(true);
		}
		else if(nearSword && swordScene == 2 && count < 20){
			textbox.GetComponent<Text>().text = messages[count];
			if(Input.GetKey(KeyCode.Return) &&((Time.time-startTime) > 0.5f)){
				startTime = Time.time;
				count ++;
				if(count >= 20){
					wall.SetActive(false);
				}
			}
		}
		else if(count < 20){
			swordScene = 0;
			bubbles[0].SetActive(false);
		}
		if(momo.GetComponent<Transform> ().position.x >=58f){
			momo.GetComponent<MomotaroBehavior> ().stop = false;
			textbox.GetComponent<Text>().text = "You are leaving the comfort of your home. Press <Enter> to begin your journey.";
			theEnd = true;
		}
		if(theEnd && Input.GetKey(KeyCode.Return)){
			SceneManager.LoadScene ("FinalLevel1");
		}
	}
	
	void FixedUpdate () {
		
		//restrict movement to one plane
		if(moveDad){
			dad.GetComponent<Transform>().position = new Vector3 (dad.GetComponent<Transform>().position.x, dad.GetComponent<Transform>().position.y, 0f);
			dadRb.velocity = new Vector3 (-1 * velocity, dadRb.velocity.y, dadRb.velocity.z);
			Vector3 s = dad.GetComponent<Transform>().localScale;
			s.x = -1;
			dad.GetComponent<Transform>().localScale = s;
			dad.GetComponent<Transform> ().rotation = new Quaternion (0f, 180f, 0f, 0f);
			if(dad.GetComponent<Transform>().position.x <= 48f){
				moveDad = false;
			}
		}
		
		if(moveBoth){
			dad.GetComponent<Transform>().position = new Vector3 (dad.GetComponent<Transform>().position.x, dad.GetComponent<Transform>().position.y, 0f);
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			dadRb.velocity = new Vector3 (velocity, dadRb.velocity.y, dadRb.velocity.z);
			momRb.velocity = new Vector3 (velocity, momRb.velocity.y, momRb.velocity.z);
			Vector3 s = dad.GetComponent<Transform>().localScale;
			s.x = -1;
			Vector3 t = transform.localScale;
			t.x = -1;
			dad.GetComponent<Transform>().localScale = s;
			transform.localScale = t;
			dad.GetComponent<Transform> ().rotation = new Quaternion (0f, 0f, 0f, 0f);
			//mom.GetComponent<Transform> ().rotation = new Quaternion (0f, 180f, 0f, 0f);
			if(mom.GetComponent<Transform> ().position.x >= 54f){
				isDone = true;
				mom.SetActive(false);
				dad.SetActive(false);
			}
		}
	}
}

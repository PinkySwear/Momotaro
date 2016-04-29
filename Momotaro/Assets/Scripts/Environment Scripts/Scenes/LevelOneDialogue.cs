using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelOneDialogue : MonoBehaviour {

	public int scene;
	public GameObject momo;
	public GameObject[] boxes;
	public GameObject textbox;
	public GameObject companion;
	public GameObject manage;
	public string[] messages;
	public float refract = 0.5f;
	
	public int count;
	private float startTime;
	private float jumpTime;
	
	public float velocity;
	public float jumpForce;
	//public bool jump = false;
	
	//private bool onSomething;
	public bool moveDog;
	private Rigidbody dogRb;
	//private CapsuleCollider myCollider;
	
	public bool seesMomo;
	
	// Use this for initialization
	void Start () {
		dogRb = GetComponent<Rigidbody> ();
		dogRb.freezeRotation = true;
		velocity = 10f;
		jumpForce = 1000f;
		messages[0] = "*huff* *huff*";
		messages[1] = "Aw, poor dog… Are you okay?";
		messages[2] = "That. Was. Awesome!";
		messages[3] = "Wh-whoah! You can talk?!";
		messages[4] = "One, yes. Two, you saved me from those Oni! And three, you are now my BEST FRIEND!";
		messages[5] = "Well, um. You’re welcome! I’m on a mission. To fight the Oni. And also maybe find out where I came from.";	
		messages[6] = "Amazing! So, where are we going next?";
		messages[7] = "We? I don’t know how to put this, but… I’m more of a solo hero.";
		messages[8] = "Oh. Oh! Of course. Yes. I… understand. Well. Goodbye then.";
		messages[9] = "Goodbye… uh… what’s your name?";
		messages[10] = "Dog.";
		messages[11] = "Okay then. I’m Momotaro, but you can call me Momo."; 
		messages[12] = "Momo."; 
		messages[13] = "Yes. Well, goodbye then, Dog!";
		messages[14] = "Goodbye, Momo!";
		messages[15] = "Hey! Dog!";
		messages[16] = "Oh, hey! Momo! I, uh, didn’t see you there.";
		messages[17] = "Are you following me?";
		messages[18] = "What?? Following? You?? Me?? I… What?";
		messages[19] = "Look… Just stop following me, Dog.";
		messages[20] = "Hey! You! Oni! Get down from there!";
		messages[21] = "Momo! I can help!";
		boxes[0].SetActive(false);
		boxes[1].SetActive(false);
		startTime = Time.time;
		jumpTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetKey(KeyCode.X)){
//			SceneManager.LoadScene ("AlphaDemo");
//		}
		if((momo.GetComponent<Transform>().position.x <= transform.position.x) &&
		   (momo.GetComponent<Transform>().position.x + 2 >= transform.position.x)){
			seesMomo = true;
			//box1.SetActive(true);
			//text1.GetComponent<Text>().text = "Press 'Enter' to Eat Breakfast";
		}
		if(scene ==1 && seesMomo){
			momo.GetComponent<MomotaroBehavior> ().stop = true;
			if((count % 2) ==0){
				boxes[0].SetActive(true);
				boxes[1].SetActive(false);
			}
			else{
				boxes[1].SetActive(true);
				boxes[0].SetActive(false);
			}
			textbox.GetComponent<Text>().text = messages[count];
			if(Input.GetKey(KeyCode.Return) &&((Time.time-startTime) > refract)){
				startTime = Time.time;
				count ++;
				if(count >= 15){
					scene ++;
					boxes[1].SetActive(false);
					boxes[0].SetActive(false);
					textbox.GetComponent<Text>().text = "";
					momo.GetComponent<MomotaroBehavior> ().stop = false;
				}
			}
		}
		if(scene==3){
			if((count % 2) ==0){
				boxes[0].SetActive(true);
				boxes[1].SetActive(false);
			}
			else{
				boxes[1].SetActive(true);
				boxes[0].SetActive(false);
			}
			textbox.GetComponent<Text>().text = messages[count];
			if(Input.GetKey(KeyCode.Return) &&((Time.time-startTime) > refract)){
				startTime = Time.time;
				count ++;
				if(count >= 20){
					scene ++;
					boxes[1].SetActive(false);
					boxes[0].SetActive(false);
					textbox.GetComponent<Text>().text = "";
					momo.GetComponent<MomotaroBehavior> ().stop = false;
				}
			}
		}
		
		if(scene==5){
			if((count % 2) ==1){
				boxes[0].SetActive(true);
				boxes[1].SetActive(false);
			}
			else{
				boxes[1].SetActive(true);
				boxes[0].SetActive(false);
			}
			textbox.GetComponent<Text>().text = messages[count];
			if(Input.GetKey(KeyCode.Return) &&((Time.time-startTime) > refract)){
				startTime = Time.time;
				count ++;
				if(count >= 22){
					scene ++;
					boxes[1].SetActive(false);
					boxes[0].SetActive(false);
					textbox.GetComponent<Text>().text = "";
					momo.GetComponent<MomotaroBehavior> ().stop = false;
					companion.SetActive(true);
					manage.GetComponent<CharacterManager> ().beginScene = true;
					gameObject.SetActive(false);
				}
			}
		}
		
	}
	
	void FixedUpdate () {
		
		if (moveDog) {
			transform.rotation = new Quaternion (0f, 0f, 0f,0f);
			//restrict movement to one plane
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			dogRb.velocity = new Vector3 (velocity, dogRb.velocity.y, dogRb.velocity.z);
			Vector3 s = transform.localScale;
			s.x = 1;
			transform.localScale = s;
			//dogs.GetComponent<Transform> ().position = new Vector3 (75f,transform.position.y,transform.position.z);
	
			if((Time.time - jumpTime) >= 1f){
				Debug.Log("Jump!");
				transform.position = new Vector3 (transform.position.x, transform.position.y+1f, 0f);
				jumpTime = Time.time;
				dogRb.AddForce (Vector3.up * jumpForce);
			}
		}
		velocity = 10f;
		
	}
}


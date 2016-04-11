using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class KitchenSceneDialogue : MonoBehaviour {

	public float velocity;
	public float jumpForce;
	public int dialogue;
	public int speaker;
	public float letterPause;
	public bool movingLeft;
	public bool movingRight;
	public bool jump = false;
	
	public GameObject momo;
	public GameObject dad;
	public GameObject text1;
	public GameObject box1;
	public AudioSource[] typeSFX;
	public GameObject[] text;
	public GameObject[] box;
	
	private bool onSomething;
	private bool enterRoom;
	private bool moveDad;
	public bool moveBoth;
	private bool seesMomo;
	private bool stop = false;
	private float refract = 1f;
	private float refractTime;
	private float startTime;
	private char[] message;
	private int count;
	private Rigidbody myRb;
	private Rigidbody dadRb;
	private Rigidbody momRb;
	private CapsuleCollider myCollider;
	private bool isDone;

	// All Dialogues
	string[] messages = new string[16];
	
	// Mom dialogue
	string message0a = "How did you sleep, my little peach?";
	string message0b = "You mean the Oni?";
	string message0c = "Yes, Momotaro, they are. Ever since you came to us from the heavens, our little gift inside a big peach. ";
	string message0d = "From a peach! You know that!";
	string message0e = "What's wrong?";
	string message0f = "Oni?! But they haven’t been seen around here since…";
	string message0g = "Absolutely not. This is for adults. You stay here, and don’t leave the house.";
	
	// Dad dialogue
	string message1a = "Honey!";
	string message1b = "Oni! Sighted in the hills outside of town.";
	string message1c = "There’s no time to talk, everyone is already assembling in the village square to decide what to do. Let’s go.";
	
	// Momo dialogue
	string message2a = "I had that dream again, Mom. The one with those demons.";
	string message2b = "Yeah. Are they all gone?";
	string message2c = "Mom, where did I come from?";
	string message2d = "Well, yeah, but… where did the peach come from?";
	string message2e = "Dad, what’s going on?!";
	string message2f = "Can I come, Dad?";
	
	// Use this for initialization
	void Start () {
		typeSFX = gameObject.GetComponents<AudioSource>();
		messages[0] = message0a;
		messages[1] = message2a;
		messages[2] = message0b;
		messages[3] = message2b;
		messages[4] = message0c;
		messages[5] = message2c;
		messages[6] = message0d;
		messages[7] = message2d;
		messages[8] = message1a;
		messages[9] = message0e;
		messages[10] = message1b;
		messages[11] = message0f;
		messages[12] = message2e;
		messages[13] = message1c;
		messages[14] = message2f;
		messages[15] = message0g;
		startTime = Time.time;
		myRb = momo.GetComponent<Rigidbody> ();
		myCollider = momo.GetComponent<CapsuleCollider> ();
		myRb.freezeRotation = true;
		dadRb = dad.GetComponent<Rigidbody> ();
		dadRb.freezeRotation = true;
		momRb = GetComponent<Rigidbody> ();
		momRb.freezeRotation = true;
		velocity = 10f;
		jumpForce = 1000f;
		refractTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 colliderCenter = myCollider.bounds.center;
		Vector3 right = colliderCenter + Vector3.right * myCollider.bounds.extents.x * 0.95f;
		Vector3 left = colliderCenter - Vector3.right * myCollider.bounds.extents.x * 0.95f;
		onSomething = Physics.Linecast (right, right + (Vector3.down * myCollider.bounds.extents.y * 1.001f), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.down * myCollider.bounds.extents.y * 1.001f), 1 << LayerMask.NameToLayer ("Obstacle"));
		movingLeft = false;
		movingRight = false;
		
		if(!stop){
			box[0].SetActive(false);
			box[1].SetActive(false);
			box[2].SetActive(false);
			text[0].GetComponent<Text>().text = "";
			text[1].GetComponent<Text>().text = "";
			text[2].GetComponent<Text>().text = "";
			if((momo.GetComponent<Transform>().position.x <= transform.position.x) &&
				(momo.GetComponent<Transform>().position.x + 5 >= transform.position.x) && !isDone){
				seesMomo = true;
				box1.SetActive(true);
				text1.GetComponent<Text>().text = "Press 'Enter' to Eat Breakfast";
			}
			else{
				seesMomo = false;
				box1.SetActive(false);
				text1.GetComponent<Text>().text = "";
			}
			if(seesMomo && Input.GetKey(KeyCode.Return)){
				text1.GetComponent<Text>().text = "Press 'Enter' to Skip to New Dialogue";
				stop = true;
				//momo.GetComponent<MomotaroBehavior>().stop = true;
				refractTime = Time.time;
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				movingLeft = true;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				movingRight = true;
			}
			if (Input.GetKeyDown(KeyCode.UpArrow) && onSomething) {
				jump = true;
				//Debug.Log("Jump initiated!");
			}
		}
		else{
			if(Input.GetKey(KeyCode.Return) && dialogue < 16 && (Time.time-refractTime > refract)){
				refractTime = Time.time;
				box[0].SetActive(false);
				box[1].SetActive(false);
				box[2].SetActive(false);
				text[0].GetComponent<Text>().text = "";
				text[1].GetComponent<Text>().text = "";
				text[2].GetComponent<Text>().text = "";
				message = messages[dialogue].ToCharArray();
				count = 0;
				if(dialogue == 8 || dialogue == 10 || dialogue == 13){
					speaker = 2;
				}
				else if(dialogue == 1 || dialogue == 3 || dialogue == 5 ||
				        dialogue == 7 || dialogue == 12 || dialogue == 14){
					speaker = 1;		
					}
				else{
					speaker = 0;
				}
				Debug.Log(dialogue);
				dialogue++;
			}
			
			if((Time.time-startTime > letterPause) && message != null && message.Length > count){
				box[speaker].SetActive(true);
				text[speaker].GetComponent<Text>().text += message[count];
				typeSFX[speaker].Play();
				if(message[count] == '.' || message[count] == '?' || message[count] == '!'){
					letterPause = 0.3f;
				}
				else if(message[count] == ','){
					letterPause = 0.15f;
				}
				else{
					letterPause = 0.075f;
				}
				count ++;
				startTime = Time.time;
			}
			
			if(dialogue == 9 && !enterRoom){
				Debug.Log("HI IM MR MEESEEKS");
				moveDad = true;
				enterRoom = true;
			}
			if(dialogue >= 16 && !moveBoth && count >= 77){
				moveBoth = true;
				dialogue++;
			}
			
			if(dialogue > 16){
				stop = false;
			}
		}
		dad.GetComponent<Transform>().position = new Vector3 (dad.GetComponent<Transform>().position.x, dad.GetComponent<Transform>().position.y, 2.25f);
		transform.position = new Vector3 (transform.position.x, transform.position.y, 2.25f);
	}
	
	void FixedUpdate () {
		momo.GetComponent<Transform>().position = new Vector3 (momo.GetComponent<Transform>().position.x, momo.GetComponent<Transform>().position.y, 0f);
		if (movingLeft) {
			//restrict movement to one plane
			momo.GetComponent<Transform>().position = new Vector3 (momo.GetComponent<Transform>().position.x, momo.GetComponent<Transform>().position.y, 0f);
			myRb.velocity = new Vector3 (-1 * velocity, myRb.velocity.y, myRb.velocity.z);
			Vector3 s = momo.GetComponent<Transform>().localScale;
			s.x = -1;
			momo.GetComponent<Transform>().localScale = s;
		}
		if (movingRight) {
			momo.GetComponent<Transform>().position = new Vector3 (momo.GetComponent<Transform>().position.x, momo.GetComponent<Transform>().position.y, 0f);
			myRb.velocity = new Vector3 (velocity, myRb.velocity.y, myRb.velocity.z);
			Vector3 s = momo.GetComponent<Transform>().localScale;
			s.x = 1;
			momo.GetComponent<Transform>().localScale = s;
		}
		if (moveDad) {
			//restrict movement to one plane
			dad.GetComponent<Transform>().position = new Vector3 (dad.GetComponent<Transform>().position.x, dad.GetComponent<Transform>().position.y, 0f);
			dadRb.velocity = new Vector3 (-1 * velocity, dadRb.velocity.y, dadRb.velocity.z);
			Vector3 s = dad.GetComponent<Transform>().localScale;
			s.x = -1;
			dad.GetComponent<Transform>().localScale = s;
			if(dad.GetComponent<Transform>().position.x <= 46f){
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
			if(dad.GetComponent<Transform>().position.x >= 57f){
				dad.SetActive(false);
			}
			if(transform.position.x >= 57f){
				isDone = true;
				GetComponent<SpriteRenderer> ().enabled = false;
				GetComponent<CapsuleCollider> ().enabled = false;
			}
		}
		
		if(!moveDad && !moveBoth){
			dadRb.velocity = new Vector3(0f, dadRb.velocity.y, dadRb.velocity.z);
		}

		momo.GetComponent<Transform>().localScale = new Vector3 (momo.GetComponent<Transform>().localScale.x, 1f, 1f);
		velocity = 10f;
		if (jump && onSomething && Mathf.Abs(myRb.velocity.y) < 0.01f) {
			myRb.AddForce (Vector3.up * jumpForce);
			jump = false;
		}
		if (!movingRight && !movingLeft) {
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
		//transform.rotation = Quaternion.Euler (Vector3.zero);
	}
}

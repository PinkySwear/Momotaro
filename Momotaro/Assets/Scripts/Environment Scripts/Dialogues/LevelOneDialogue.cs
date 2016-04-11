using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelOneDialogue : MonoBehaviour {

	public bool hasDefeatOni;
	public float velocity;
	public float jumpForce;
	//public bool movingRight;
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
		if(!stop){
			if((momo.GetComponent<Transform>().position.x <= transform.position.x) &&
			   (momo.GetComponent<Transform>().position.x + 2 >= transform.position.x)){
				seesMomo = true;
			}
			
			if(hasDefeatOni && seesMomo){
				stop = true;
				momo.GetComponent<MomotaroBehavior> ().stop = true;
			}
			
			if((momo.GetComponent<Transform> ().position.x >= 131) &&
			   !(transform.position.x >= 131f)){
				Debug.Log("no move");
				transform.rotation = Quaternion.Euler (new Vector3 (0f, 180f, 0f));
				moveDog = true;
			}
			if(momo.GetComponent<Transform> ().position.x >= 138 &&
			   !hasDefeatOni){
				Debug.Log("no stop");
				stop = true;
				momo.GetComponent<MomotaroBehavior> ().stop = true;
			}
		}
		else{
			if(Input.GetKey(KeyCode.Return) && 
			   ((Time.time-startTime) >= pause) &&
			   (count != 16) && (count != 22)){
				count ++;
				Debug.Log(count);
				wordCount = 0;
				startTime = Time.time;
				refractTime = Time.time;
				if(((count % 2) == 1 && (count <= 16)) ||
				   ((count % 2) == 0 && (count > 16))){
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
					(count == 16 || count == 22)){
				Debug.Log("Stop?");
				stop = false;
				momo.GetComponent<MomotaroBehavior> ().stop = false;
			}
			if(count != 16 && count != 22){
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
			}
			if((Time.time-refractTime) >= refract){
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
			if(count == 16){
				dogDiag[dogIdx].SetActive(false);
				hasDefeatOni = false;
			}
			if(count == 22){
				momoDiag[momoIdx].SetActive(false);
				hasDefeatOni = true;
				momo.GetComponent<MomotaroBehavior> ().stop = false;
			}
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
			if(transform.position.x >= 131f){
				moveDog = false;
				count = 17;
				dogs.GetComponent<Transform> ().position = new Vector3 (30f,transform.position.y,transform.position.z);
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


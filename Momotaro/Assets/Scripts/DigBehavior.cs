using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DigBehavior : MonoBehaviour {

	public GameObject dog;
	public GameObject textbox;
	public int horizontal;
	public float forward;
	public float limit;

	private bool inDigZone;
	private bool locked;
	
	// Use this for initialization
	void Start () {
		textbox.GetComponent<Text>().text = "";
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dogPos = dog.GetComponent<Transform>().position;
		if(inDigZone && Input.GetKey (KeyCode.S) && !locked){
			if(horizontal == 1){
				transform.position = new Vector3 (transform.position.x+(0.1f*forward),transform.position.y,transform.position.z);
			}
			else if (horizontal == 0){
				transform.position = new Vector3 (transform.position.x,transform.position.y-(0.1f*forward),transform.position.z);
			}
			else{
				transform.position = new Vector3 (transform.position.x+(0.1f*forward),transform.position.y+(0.1f*forward),transform.position.z);
			}
		}
		if((horizontal==0) && !locked){
			if(((transform.position.y + 2) >= dogPos.x) && (Mathf.Abs(transform.position.x - 2) >= Mathf.Abs(dogPos.x))){
				textbox.GetComponent<Text>().text = "Press S to dig down!";
				inDigZone = true;
			}
			else{
				textbox.GetComponent<Text>().text = "";
				inDigZone = false;
			}
			if(transform.position.y <= limit){
				locked = true;
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<BoxCollider>().enabled = false;
			}
		}
		else if((horizontal==1) && !locked){
			if(((transform.position.x - 2) <= dogPos.x) && (transform.position.x >= dogPos.x) && (transform.position.y >= dogPos.y)){
				Debug.Log("hello?");
				textbox.GetComponent<Text>().text = "Press S to dig across!";
				inDigZone = true;
			}
			else{
				textbox.GetComponent<Text>().text = "";
				inDigZone = false;
			}
			if(transform.position.x >= limit){
				locked = true;
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<BoxCollider>().enabled = false;
			}
		}
		else if(!locked){
			if(((transform.position.x - 2) <= dogPos.x) && (transform.position.x >= dogPos.x) && (transform.position.y >= dogPos.y)){
				Debug.Log("hello?");
				textbox.GetComponent<Text>().text = "Press S to dig across!";
				inDigZone = true;
			}
			else{
				textbox.GetComponent<Text>().text = "";
				inDigZone = false;
			}
			if(transform.position.x >= limit){
				locked = true;
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<BoxCollider>().enabled = false;
			}
		}
	}
}

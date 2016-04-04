using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractionControl : MonoBehaviour {

	public GameObject avatar;
	public GameObject door;
	public GameObject textbox;
	public int num;
	public string message;
	public bool activated = false;
	
	private bool seesAvatar = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!activated){
			if((avatar.GetComponent<Transform>().position.x <= transform.position.x) &&
				(avatar.GetComponent<Transform>().position.x + 2 >= transform.position.x)){
				seesAvatar = true;
				textbox.GetComponent<Text>().text = message;
			}
			else{
				seesAvatar = false;
				textbox.GetComponent<Text>().text = "";
			}
			if(seesAvatar && Input.GetKey(KeyCode.Return)){
				textbox.GetComponent<Text>().text = "";
				if(num == 1){
					door.GetComponent<Transform>().position = new Vector3 (door.GetComponent<Transform>().position.x,door.GetComponent<Transform>().position.y+10,door.GetComponent<Transform>().position.z);
				}
				else if(num == 2){
					door.GetComponent<Transform>().position = new Vector3 (door.GetComponent<Transform>().position.x,door.GetComponent<Transform>().position.y-10,door.GetComponent<Transform>().position.z);
				}
				else if(num == 0){
					Debug.Log("You Won!");
				}
				activated = true;
			}
		}
	}
}

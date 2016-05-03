using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FriskControl : MonoBehaviour {

	public GameObject camera;

	public bool moveUP;
	public bool moveDown;
	public bool moveLeft;
	public bool moveRight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)){
			moveUP = true;
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)){
			moveUP = false;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)){
			moveDown = true;
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)){
			moveDown = false;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)){
			moveRight = true;
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)){
			moveRight = false;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)){
			moveLeft = true;
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)){
			moveLeft = false;
		}
		
		
		
		if(moveUP){
			transform.position = new Vector3 (transform.position.x, transform.position.y+0.1f, transform.position.z);
		}
		if(moveDown){
			transform.position = new Vector3 (transform.position.x, transform.position.y-0.1f, transform.position.z);
		}
		if(moveRight){
			transform.position = new Vector3 (transform.position.x+0.1f, transform.position.y, transform.position.z);
		}
		if(moveLeft){
			transform.position = new Vector3 (transform.position.x-0.1f, transform.position.y, transform.position.z);
		}
		
		camera.GetComponent<Transform>().position = new Vector3 (transform.position.x, transform.position.y, -7);
		transform.rotation = Quaternion.Euler (0f,0f,0f);
		transform.position = new Vector3 (transform.position.x,transform.position.y,-1f);
	}
	
	void OnCollisionEnter(UnityEngine.Collision collision)
	{
        Debug.Log("hit");
	}
}

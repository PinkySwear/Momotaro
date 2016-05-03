using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class KitchenInteract : MonoBehaviour {

	private bool seesMomo;
	private bool momGone;
	private int next;
	private bool journey;
	private float lapse;

	public GameObject sword;
	public GameObject momo;
	public GameObject mom;
	public GameObject wall;
	
	public GameObject text;
	public GameObject box;
	public GameObject text2;
	public GameObject box2;

	// Use this for initialization
	void Start () {
		box.SetActive(false);
		text.GetComponent<Text>().text = "";
		box2.SetActive(false);
		text2.GetComponent<Text>().text = "";
		lapse = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(mom.GetComponent<KitchenSceneDialogue> ().moveBoth){
			momGone = true;
		}
		if((momo.GetComponent<Transform>().position.x >= sword.GetComponent<Transform>().transform.position.x) &&
		   (momo.GetComponent<Transform>().position.x - 5 <= sword.GetComponent<Transform>().transform.position.x)){
			seesMomo = true;
			box.SetActive(true);
			if(next == 0){
				text.GetComponent<Text>().text = "Press 'Enter' to Examine Dad's Sword";
			}
		}
		else if(momo.GetComponent<Transform>().position.x >= 57f){
			box.SetActive(true);
			text.GetComponent<Text>().text = "Press 'Enter' to Begin Your Journey";
			journey = true;
		}
		
		else{
			box.SetActive(false);
			text.GetComponent<Text>().text = "";
			box2.SetActive(false);
			text2.GetComponent<Text>().text = "";
		}
		
		if(journey && Input.GetKey(KeyCode.Return)){
			SceneManager.LoadScene ("AlphaLevel");
		}
		
		if(seesMomo && Input.GetKey(KeyCode.Return) && (Time.time - lapse) >= 1f){
			lapse = Time.time;
			if(momGone){
				Debug.Log("HERE!");
				next ++;
				next = next % 4;
				if(next == 1){
					box.SetActive(true);
					text.GetComponent<Text>().text = "I am Dad’s sword. I will all demons banish.";
				}
				else if(next == 2){
					box.SetActive(true);
					text.GetComponent<Text>().text = "When I’m done just say the word. And I will vanish.";
				}
				else if(next == 3){
					box.SetActive(true);
					text.GetComponent<Text>().text = "P.S. Use spacebar to wield me.";
				}
				else if(next == 0){
					sword.SetActive(false);
					wall.SetActive(false);
					box.SetActive(false);
					text.GetComponent<Text>().text = "";
				}
			}
			else if(mom.GetComponent<KitchenSceneDialogue> ().dialogue < 1){
				next ++;
				next = next % 2;
				Debug.Log(next);
				if(next == 1){
					box2.SetActive(true);
					text2.GetComponent<Text>().text = "Please don't touch that Honey!";
				}
				else{
					box2.SetActive(false);
					text2.GetComponent<Text>().text = "";
				}
			}
		}
	}
}

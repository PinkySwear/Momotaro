using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class JerryDialogue : MonoBehaviour {

	public GameObject momo;
	public GameObject textbox;
	public AudioSource typeSFX;
	public GameObject box;
	
	//public float audio;
	//public bool quiet;
	public float letterPause = 0.1f;
	
	private bool stop;
	private bool seesMomo;
	private bool initial = true;
	private float startTime;
	private int scene;
	private int diag;
	private char[] message;
	private int count;

	string message1a = "Hey! You there!";
	string message1b = "Stop right there! Or I'll have to...I dunno curse you, yeah!";
	string message1c = "Alright, alright, I can't really do that, but can you at least help me?";
	string message1d = "I left my prized rice-cake in the Stables down yonder. If I don't get it back, I don't know what I'll do!";
		
	string message2a = "Me? Oni? Of course not! Have you seen me? I'm pink!";
	string message2b = "See! Look at my business card!";
	string message2c = "* He hands you his business card *";
	string message2d = "* It reads; 'Jerry Z. Shminklestein: Non-Oni Oni, Specializes in Being Pink' *";
		
	string message3 = "My Rice Cake! Woowie! Thanks friend! Yippie!";
	
	
	// Use this for initialization
	void Start () {
		typeSFX = gameObject.GetComponent<AudioSource>();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Time.time);
		if(!stop){
			if((momo.GetComponent<Transform>().position.x <= transform.position.x) &&
				(momo.GetComponent<Transform>().position.x + 5 >= transform.position.x)){
				seesMomo = true;
				box.SetActive(true);
			}
			else{
				box.SetActive(false);
			}
			if(seesMomo){
				if(initial && diag == 0){
					momo.GetComponent<MomotaroBehavior>().stop = true;
					message = message1a.ToCharArray();
					stop = true;
					startTime = Time.time;
					
				}
			}
		}
		else{
			if((Time.time-startTime > letterPause) && message.Length > count){
				textbox.GetComponent<Text>().text += message[count];
				typeSFX.Play();
				Debug.Log(message[count]);
				count ++;
				startTime = Time.time;
			}
			else if(message.Length <= count){
				momo.GetComponent<MomotaroBehavior>().stop = false;
				stop = false;
				initial = false;
			}
		}
	}
}

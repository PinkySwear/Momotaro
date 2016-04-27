using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class Timer : MonoBehaviour {

    public AudioClip menuSound;

    AudioSource audio;

    public GameObject Overlay;
   
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		Invoke( "ChangeLevel", 4.8f );
		Invoke ("PlayMenu", 4.7f);
 }
 
 void ChangeLevel() {
		SceneManager.LoadScene ("Menu"); 
 	}

 void PlayMenu(){
		audio.PlayOneShot(menuSound, 0.6F);
		Debug.Log("menu sound called");
 		}

}


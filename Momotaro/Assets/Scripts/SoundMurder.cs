using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]



public class SoundMurder : MonoBehaviour {

	public AudioClip menuSound;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

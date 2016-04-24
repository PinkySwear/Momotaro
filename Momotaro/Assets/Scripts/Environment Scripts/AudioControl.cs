using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioControl : MonoBehaviour {

	public GameObject momo;
	public GameObject dog;
	public GameObject bird;
	public GameObject monkey;
	public AudioSource[] aSources;
	public GameObject[] badies;
	public float start;
	
	private bool pressed;
	public int health;
	public int[] damages;
	public int[] identity;

	// Use this for initialization
	void Start () {
		aSources = gameObject.GetComponents<AudioSource>();
		health = 6;
		for (int i=0;i < damages.Length; i++){
			damages[i] = 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(momo.GetComponent<MomotaroBehavior>().controlling){
			if((momo.GetComponent<MomotaroBehavior>().movingLeft || momo.GetComponent<MomotaroBehavior>().movingRight) &&
			   ((Time.time-start) > aSources[1].clip.length) && momo.GetComponent<MomotaroBehavior>().onSomething){
				start = Time.time;
				aSources[1].Play();
			}
			if(momo.GetComponent<MomotaroBehavior>().jump && momo.GetComponent<MomotaroBehavior>().onSomething){
				aSources[2].Play();
			}
			if(health > momo.GetComponent<MomotaroBehavior>().health){
				health --;
				aSources[3].Play();
			}
			if(momo.GetComponent<MomotaroBehavior>().justAttacked){
				aSources[4].Play();
			}
		}
		else{
			Debug.Log(dog.GetComponent<DogFollow>().hasBark);
			if(dog.GetComponent<DogFollow>().hasBark){
				aSources[5].Play();
			}
		}
		if(Input.GetKeyDown (KeyCode.S) && dog.activeSelf){
			aSources[6].Play();
		}
		if(Input.GetKeyDown (KeyCode.D) && dog.activeSelf){
			aSources[7].Play();
		}
		if(Input.GetKeyDown (KeyCode.A) && monkey.activeSelf){
			aSources[6].Play();
		}
		if(Input.GetKeyDown (KeyCode.W) && bird.activeSelf){
			aSources[7].Play();
		}
		for (int i=0;i < badies.Length; i++){
			if(badies[i].GetComponent<EnemyBehavior> ().health == 0 && badies[i].GetComponent<EnemyBehavior> ().health != damages[i]){
				if(identity[i] == 0){
					aSources[9].Play();
				}
				else{
					aSources[11].Play();
				}
				damages[i] = badies[i].GetComponent<EnemyBehavior> ().health;
			}
			else if(badies[i].GetComponent<EnemyBehavior> ().health != damages[i]){
				if(identity[i] == 0){
					aSources[8].Play();
				}
				else{
					aSources[10].Play();
				}
				damages[i] = badies[i].GetComponent<EnemyBehavior> ().health;
			}
		}
	}
}

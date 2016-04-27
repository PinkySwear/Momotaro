using UnityEngine;
using System.Collections;

public class GooBehavior : MonoBehaviour {

	public bool active;
	public MomotaroBehavior momo;

	// Use this for initialization
	void Start () {
		active = true;
		GetComponent<SpriteRenderer> ().color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(Camera.main.transform.position, transform.position) > 1000f) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (active && other.tag == "Player") {
			momo.takeDamage (1);
			Destroy (this.gameObject);
		}
	}
	void OnCollisionEnter (Collision collision) {
		active = true;
		Destroy (this.gameObject);
	}


}

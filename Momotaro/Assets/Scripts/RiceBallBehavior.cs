using UnityEngine;
using System.Collections;

public class RiceBallBehavior : MonoBehaviour {

	public bool active;

	// Use this for initialization
	void Start () {
		active = true;
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter (Collider other) {
		if (active && other.tag == "Player") {
			MomotaroBehavior mb = other.gameObject.GetComponent<MomotaroBehavior> ();
			if (mb) {
				mb.takeDamage (-3);
				Destroy (this.gameObject);
			}
		}
	}


}

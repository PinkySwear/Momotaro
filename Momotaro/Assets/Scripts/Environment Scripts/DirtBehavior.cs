using UnityEngine;
using System.Collections;

public class DirtBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnTriggerStay(Collider other) {
//		DogFollow df = other.gameObject.GetComponent<DogFollow> ();
//		if (df != null) {
//			df.inDirt = true;
//		}
//	}
	void OnTriggerExit(Collider other) {
		DogFollow df = other.gameObject.GetComponent<DogFollow> ();
		if (df != null) {
			df.inDirt = false;
		}
	}
}

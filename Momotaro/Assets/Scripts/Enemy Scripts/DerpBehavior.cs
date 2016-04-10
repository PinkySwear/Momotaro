using UnityEngine;
using System.Collections;

public class DerpBehavior : EnemyBehavior {

	bool hitSomething;

	// Use this for initialization
	void Start () {
		StartP ();
		health = 2;
		velocity = 3f;
		jumpForce = 1000f;
		movingLeft = true;
	}

	void Update () {


		UpdateP ();

	}


	void attack () {
		//LITERALLY DO JACKSHIT
	}

	void OnTriggerEnter (Collider other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Detector" || LayerMask.LayerToName (other.gameObject.layer) == "Enemy") {
			return;
		}
		if (!hitSomething) {
			hitSomething = true;
			if (movingLeft) {
				movingLeft = false;
				movingRight = true;
			}
			else if (movingRight) {
				movingRight = false;
				movingLeft = true;
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (hitSomething) {
			hitSomething = false;
		}
	}

}

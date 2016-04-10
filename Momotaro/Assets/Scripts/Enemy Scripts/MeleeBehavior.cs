using UnityEngine;
using System.Collections;

public class MeleeBehavior : EnemyBehavior {

	bool hitSomething;
	float attackCoolDown;
	bool nearPlayer;

	// Use this for initialization
	void Start () {
		StartP ();
		health = 5;
		velocity = 5f;
		jumpForce = 1000f;
		movingLeft = true;
		nearPlayer = false;
	}

	void Update () {
		if (attackCoolDown > 0f) {
			attackCoolDown -= Time.deltaTime;
		}

		if (nearPlayer) {
			movingLeft = false;
			movingRight = false;
			if (attackCoolDown <= 0f) {
				attack ();
			}

		}
		else {
			movingLeft = true;
		}

		UpdateP ();
	}

	void attack () {
		Debug.Log ("I ATTACKED");
		attackCoolDown = 2f;

	}

	void OnTriggerStay (Collider other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Detector" || LayerMask.LayerToName (other.gameObject.layer) == "Enemy") {
			return;
		}

		if (other.tag == "Player" && !nearPlayer) {
			nearPlayer = true;
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
		
		if (other.tag == "Player" && nearPlayer) {
			nearPlayer = false;
		}

		if (hitSomething) {
			hitSomething = false;
		}
	}
}

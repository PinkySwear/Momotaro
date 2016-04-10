using UnityEngine;
using System.Collections;

public class MeleeBehavior : EnemyBehavior {

	float attackCoolDown;
	bool nearPlayer;

	int numThingsInTheWay;
	bool somethingInTheWay;

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

		if (numThingsInTheWay > 0) {
			movingLeft = !movingLeft;
			movingRight = !movingRight;
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
//		Debug.Log (numThingsInTheWay);
//		Debug.Log (nearPlayer);

	}

	void attack () {
//		Debug.Log ("I ATTACKED");
		attackCoolDown = 2f;

	}

	void OnTriggerEnter (Collider other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Detector" || LayerMask.LayerToName (other.gameObject.layer) == "Enemy") {
			return;
		}
		if (other.tag == "Player" && !nearPlayer) {
			nearPlayer = true;
		}
		if (other.tag == "Follower") {
			return;
		}
		numThingsInTheWay++;
	}

	void OnTriggerExit (Collider other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Detector" || LayerMask.LayerToName (other.gameObject.layer) == "Enemy") {
			return;
		}
		if (other.tag == "Player" && nearPlayer) {
			nearPlayer = false;
		}
		if (other.tag == "Follower") {
			return;
		}
		numThingsInTheWay--;	
	}
}

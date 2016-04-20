﻿using UnityEngine;
using System.Collections;

public class DerpBehavior : EnemyBehavior {

	public int numThingsInTheWay;
	bool somethingInTheWay;

	// Use this for initialization
	void Start () {
		StartP ();
		health = 2;
		velocity = 3f + Random.value;
		jumpForce = 1000f;
		movingLeft = true;
		numThingsInTheWay = 0;
	}

	void Update () {

		if (numThingsInTheWay > 0) {
			movingLeft = !movingLeft;
			movingRight = !movingRight;
		}

		UpdateP ();
	}


	void attack () {
		//LITERALLY DO JACKSHIT
	}

	void OnTriggerEnter (Collider other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Detector" || LayerMask.LayerToName (other.gameObject.layer) == "Enemy") {
			return;
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
		if (other.tag == "Follower") {
			return;
		}
		numThingsInTheWay--;
	}

}

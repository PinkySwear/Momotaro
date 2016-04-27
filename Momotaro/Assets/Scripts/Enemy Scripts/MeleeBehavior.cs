using UnityEngine;
using System.Collections;

public class MeleeBehavior : EnemyBehavior {

	float attackCoolDown;
	bool nearPlayer;

	private float idleTime;
	private float wanderTime;

	int numThingsInTheWay;
	bool somethingInTheWay;




	//current enemy state 0:default, 1:chasing, 2:in combat
	int state;

	// Use this for initialization
	void Start () {

		StartP ();

		health = 3;
		velocity = 4f;
		jumpForce = 1000f;
		//		movingLeft = true;
		nearPlayer = false;
		state = 0;
		idleTime = 0.1f;
	}

	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("meleeHurt")) {
			anim.SetBool ("hurt", false);
			anim.SetBool ("walking", false);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("meleeKnockBack")) {
			anim.SetBool ("knockback", false);
			anim.SetBool ("walking", false);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("meleePoopedOn")) {
			anim.SetBool ("poopedOn", false);
			anim.SetBool ("knockback", false);
			anim.SetBool ("walking", false);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("meleeSlip")) {
			anim.SetBool ("slip", false);
			anim.SetBool ("knockback", false);
			anim.SetBool ("walking", false);
		}

		if (attackCoolDown > 0f) {
			attackCoolDown -= Time.deltaTime;
		}
		if (idleTime > 0f) {
			idleTime -= Time.deltaTime;
			if (idleTime <= 0f) {
				wanderTime = Random.value * 10f + 2f;
				anim.SetBool ("walking", true);
			}
		}
		else if (wanderTime > 0f) {
			wanderTime -= Time.deltaTime;
			if (wanderTime <= 0f) {
				idleTime = Random.value * 5f + 1f;
				anim.SetBool ("walking", false);
			}
		}

		state = 0;

		if (nearPlayer && !stunned && !isDead) {
			state = 1;
		}

		if (state == 0) {

			if (idleTime > 0f) {
				movingLeft = false;
				movingRight = false;
			}
			else if (wanderTime > 0f) {
				if (!movingLeft && !movingRight) {
					if (Random.value > 0.5f) {
						movingLeft = true;
						movingRight = false;
					}
					else {
						movingLeft = false;
						movingRight = true;
					}
				}
				if (numThingsInTheWay > 0) {
					movingLeft = !movingLeft;
					movingRight = !movingRight;
				}
			}
		}

		if (state == 1) {
			movingLeft = false;
			movingRight = false;
			Debug.Log ("IN STATE 1");
			Debug.Log (attackCoolDown);
			Debug.Log (!momo.anim.GetCurrentAnimatorStateInfo (0).IsName ("momoHurt"));
			Debug.Log (momo.invuln <= 0f);
			if (attackCoolDown <= 0f && !momo.anim.GetCurrentAnimatorStateInfo (0).IsName ("momoHurt") && momo.invuln <= 0f) {
				attack ();
			}
		}

		//		if (attackCoolDown > 0f) {
		//			attackCoolDown -= Time.deltaTime;
		//		}
		//
		//		if (numThingsInTheWay > 0) {
		//			movingLeft = !movingLeft;
		//			movingRight = !movingRight;
		//		}
		//		if (nearPlayer && !stunned) {
		//			movingLeft = false;
		//			movingRight = false;
		//			if (attackCoolDown <= 0f) {
		//				attack ();
		//			}
		//		}
		//		else {
		//			movingLeft = true;
		//		}
		//
		UpdateP ();

	}

	void attack () {
		momo.takeDamage (1);
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
		//		Debug.Log (other.tag);
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

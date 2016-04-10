using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public int health;
	public bool isDead;

	public bool stunned;
	public float stunDuration;

	public float velocity;
	public float jumpForce;
	protected Rigidbody myRb;
	protected bool onSomething = false;
	protected bool movingLeft;
	protected bool movingRight;
	protected bool jump = false;

	// Use this for initialization
	protected void StartP () {
		myRb = this.gameObject.GetComponent<Rigidbody> ();
		stunned = false;
		isDead = false;
	}
	
	// Update is called once per frame
	protected void UpdateP () {
		if (stunned) {
			if (stunDuration > 0f) {
				stunDuration -= Time.deltaTime;
			}
			if (stunDuration <= 0f) {
				stunned = false;
			}
		}
		myRb.freezeRotation = true;
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
		if (isDead) {
			movingLeft = false;
			movingRight = false;
			jump = false;
			transform.rotation = new Quaternion (0f, 0f, 90f, 0f);
		}

	}
		
	void FixedUpdate () {
		if (movingLeft && !isDead && !stunned) {
			//restrict movement to one plane
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			myRb.velocity = new Vector3 (-1 * velocity, myRb.velocity.y, myRb.velocity.z);
			Vector3 s = transform.localScale;
			s.x = -1;
			transform.localScale = s;
		}
		if (movingRight && !isDead && !stunned) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			myRb.velocity = new Vector3 (velocity, myRb.velocity.y, myRb.velocity.z);
			Vector3 s = transform.localScale;
			s.x = 1;
			transform.localScale = s;
		}

		if (jump && onSomething && Mathf.Abs(myRb.velocity.y) < 0.01f && !isDead && !stunned) {
			myRb.useGravity = true;
			myRb.AddForce (Vector3.up * jumpForce);

			jump = false;
		}
		if (!movingRight && !movingLeft && !stunned) {
			Debug.Log (myRb);
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
	}
		
	public void takeDamage (int dm) {
		if (health > 0) {
			health -= dm;
		}
		if (health <= 0) {
			isDead = true;
		}
	}

	public void knockBack (Vector3 force) {
		myRb.AddForce (force);
	}

	public void stun (float duration) {
		stunned = true;
		stunDuration = duration;
	}
}

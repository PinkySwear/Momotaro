using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public GameObject MomoObject;
	public MomotaroBehavior momo;
	public int health;
	public bool isDead;
	public float timeSinceDeath;

	public bool stunned;
	public float stunDuration;

	public float velocity;
	public float jumpForce;
	protected Rigidbody myRb;
	protected bool onSomething = false;
	protected bool movingLeft;
	protected bool movingRight;
	protected bool jump = false;

	public Animator anim;

	// Use this for initialization
	protected void StartP () {		
		anim = GetComponent<Animator> ();
		
		momo = MomoObject.GetComponent<MomotaroBehavior> ();
		myRb = this.gameObject.GetComponent<Rigidbody> ();
		stunned = false;
		isDead = false;
	}
	
	// Update is called once per frame
	protected void UpdateP () {

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("poof")) {
			anim.SetBool ("dead", false);
		}
		if (stunned) {
			transform.rotation = Quaternion.Euler (new Vector3(0f, stunDuration * 360f, 0f));
			if (stunDuration > 0f) {
				stunDuration -= Time.deltaTime;
			}
			if (stunDuration <= 0f) {
				stunned = false;
			}
		}
		else {
			transform.rotation = Quaternion.Euler (Vector3.zero);
		}
		myRb.freezeRotation = true;
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
		if (isDead) {
			movingLeft = false;
			movingRight = false;
			jump = false;
			if (!anim.GetBool ("dead") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("poof")) {
//				Destroy (gameObject);
				transform.position = new Vector3(0f, 0f, 500f);
			}
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
//			Debug.Log (myRb);
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
		myRb.velocity = Vector3.ClampMagnitude (myRb.velocity, 20f);
	}
		
	public void takeDamage (int dm) {
		if (anim != null) {
			Debug.Log ("set hurt to true");
			anim.SetBool ("hurt", true);
		}
		if (health > 0) {
			health -= dm;
		}
		if (health <= 0) {
			isDead = true;
			anim.SetBool ("dead", true);
		}
	}

	public void knockBack (Vector3 force) {
		if (!isDead) {
			myRb.AddForce (force);
			if (anim != null && !anim.GetBool("hurt")) {
				Debug.Log ("set knockback to true");
				anim.SetBool ("knockback", true);
			}
		}
	}

	public void stun (float duration) {
		if (!isDead) {
			stunned = true;
			stunDuration = Mathf.Max(duration, stunDuration);
		}
	}
}

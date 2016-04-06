using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour {

	public GameObject leader;
	public MomotaroBehavior momo; 


	public GameObject followInformationObject;
	public FollowInformation followInfo;



	public float velocity;
	public float specialVelocity;
	public float jumpForce;
	protected Rigidbody myRb;
	protected bool onSomething = false;
	protected bool underSomething = false;
	protected bool movingLeft;
	protected bool movingRight;
	protected bool movingDown;
	protected bool movingUp;
	protected bool jump = false;
	protected bool crouching = false;

	public bool controlling;
	public bool inParty;

	public int commandDelay;

	public Queue infoQueue;

	public CapsuleCollider myCollider;

	protected float fullHeight;

	protected Vector3 right;
	protected Vector3 left;

	protected bool specialMovement;


	// Use this for initialization
	void Start () {
//		momo = leader.GetComponent<MomotaroBehavior> ();
//		myCollider = this.gameObject.GetComponent<CapsuleCollider> ();
//		fullHeight = myCollider.bounds.extents.y;
//		//		followInfo = followInformationObject.GetComponent<FollowInformation> ();
//		infoQueue = new Queue();
//		velocity = 10f;
//		jumpForce = 1000f;
//		myRb = GetComponent<Rigidbody> ();
//		myRb.freezeRotation = true;
//		inParty = true;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
		if (!controlling && inParty) {
			if (infoQueue.Count > commandDelay) {
				FollowInformation.MovementInfo mi;
				mi = (FollowInformation.MovementInfo)infoQueue.Dequeue ();
				movingLeft = mi.movingLeft;
				movingRight = mi.movingRight;
				jump = mi.jump;
				crouching = mi.crouching;
			}
			else {
				movingLeft = false;
				movingRight = false;
				jump = false;
				crouching = false;
			}
		}

		if (movingLeft) {
			//restrict movement to one plane
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			myRb.velocity = new Vector3 (-1 * velocity, myRb.velocity.y, myRb.velocity.z);
			Vector3 s = transform.localScale;
			s.x = -1;
			transform.localScale = s;
		}
		if (movingRight) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			myRb.velocity = new Vector3 (velocity, myRb.velocity.y, myRb.velocity.z);
			Vector3 s = transform.localScale;
			s.x = 1;
			transform.localScale = s;
		}

		if (crouching) {
			transform.localScale = new Vector3 (transform.localScale.x, 0.5f, 1f);
			velocity = 5f;
		}
		else {
			transform.localScale = new Vector3 (transform.localScale.x, 1f, 1f);
			velocity = 10f;
		}

		if (jump && onSomething && Mathf.Abs(myRb.velocity.y) < 0.01f) {
			myRb.useGravity = true;
			myRb.AddForce (Vector3.up * jumpForce);

			jump = false;
		}
		if (!movingRight && !movingLeft) {
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
		if (specialMovement) {
//			myRb.velocity = new Vector3 (myRb.velocity.x, 0f, 0f);

			if (movingLeft) {
				//restrict movement to one plane
				transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
				myRb.velocity = new Vector3 (-1 * specialVelocity, myRb.velocity.y, myRb.velocity.z);
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
			}
			if (movingRight) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
				myRb.velocity = new Vector3 (specialVelocity, myRb.velocity.y, myRb.velocity.z);
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
			}

			if (movingDown) {
				myRb.velocity = new Vector3 (myRb.velocity.x, -1 * specialVelocity, myRb.velocity.z);
			}
			if (movingUp) {
				myRb.velocity = new Vector3 (myRb.velocity.x, specialVelocity, myRb.velocity.z);
			}
		}
		//transform.rotation = leader.GetComponent<Transform>().rotation;
		//transform.rotation = Quaternion.Euler (Vector3.zero);
	}
}

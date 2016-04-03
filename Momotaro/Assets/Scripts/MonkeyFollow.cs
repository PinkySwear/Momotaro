using UnityEngine;
using System.Collections;

public class MonkeyFollow : CharacterBehavior {

	public bool somethingOnLeft;
	public bool somethingOnRight;
	public Vector3 prevPos;

	// Use this for initialization
	void Start () {
		momo = leader.GetComponent<MomotaroBehavior> ();
		myCollider = this.gameObject.GetComponent<CapsuleCollider> ();
		fullHeight = myCollider.bounds.extents.y;
		//		followInfo = followInformationObject.GetComponent<FollowInformation> ();
		infoQueue = new Queue();
		velocity = 10f;
		jumpForce = 1000f;
		myRb = GetComponent<Rigidbody> ();
		myRb.freezeRotation = true;
		inParty = true;
		specialMovement = false;
		somethingOnLeft = false;
		somethingOnRight = false;
		prevPos = transform.position;


	}

	// Update is called once per frame
	void Update () {

		//		if (controlling && Vector3.Distance (leader.transform.position, transform.position) > 4f) {
		//			inParty = false;
		//		}

		if (!controlling && !inParty && Vector3.Distance (leader.transform.position, transform.position) < 1f) {
			transform.position = leader.transform.position;
			inParty = true;
			infoQueue.Clear ();
		}

		Vector3 colliderCenter = myCollider.bounds.center;
		Vector3 right = colliderCenter + Vector3.right * myCollider.bounds.extents.x * 0.95f;
		Vector3 left = colliderCenter - Vector3.right * myCollider.bounds.extents.x * 0.95f;

		Debug.DrawLine (right, right + (Vector3.down * myCollider.bounds.extents.y * 1.01f));
		Debug.DrawLine (left, left + (Vector3.down * myCollider.bounds.extents.y * 1.01f));
		Debug.DrawLine (right, right + (Vector3.up * fullHeight * 1.5f));
		Debug.DrawLine (left, left + (Vector3.up * fullHeight * 1.5f));

		onSomething = Physics.Linecast (right, right + (Vector3.down * myCollider.bounds.extents.y * 1.001f), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.down * myCollider.bounds.extents.y * 1.001f), 1 << LayerMask.NameToLayer ("Obstacle"));

		underSomething = Physics.Linecast (right, right + (Vector3.up * fullHeight * 1.5f), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.up * fullHeight * 1.5f), 1 << LayerMask.NameToLayer ("Obstacle"));
	

		movingUp = false;
		movingDown = false;
		movingLeft = false;
		movingRight = false;
		somethingOnLeft = false;
		somethingOnRight = false;

		myRb.useGravity = true;

		if (controlling && !specialMovement) {
			//			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
			if (Input.GetKey(KeyCode.LeftArrow)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
				inParty = false;
				if (myRb.velocity.x > -0.01f) {
					somethingOnLeft = true;
				}
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
				inParty = false;
				if (myRb.velocity.x * -1 > -0.01f) {
					somethingOnRight = true;
				}
			}

			if (Input.GetKeyDown(KeyCode.UpArrow) && onSomething && !crouching) {
				jump = true;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				crouching = true;
			}
			if (!Input.GetKey(KeyCode.DownArrow) && !underSomething) {
				crouching = false;
			}
		}
		if (controlling && specialMovement) {	
			specialMovement = false;
			myRb.useGravity = false;
			if (Input.GetKey (KeyCode.LeftArrow)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
				inParty = false;
				if (myRb.velocity.x > -0.01f) {
					somethingOnLeft = true;
				}
//				if (somethingOnLeft) {
//					specialMovement = true;
//				}

			}
			else {
				specialMovement = false;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
				inParty = false;
				if (myRb.velocity.x * -1 > -0.01f) {
					somethingOnRight = true;
				}
//				if (somethingOnRight) {
//					specialMovement = true;
//				}
			}
			else {
				specialMovement = false;
			}

			if (Input.GetKey(KeyCode.UpArrow)) {
				movingUp = true;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				movingDown = true;
			}
//			somethingOnLeft = false;
//			somethingOnRight = false;
		}
//		somethingOnLeft = false;
//		somethingOnRight = false;
		if (somethingOnLeft || somethingOnRight) {
			specialMovement = true;
		}

	}

//	void OnCollisionStay(Collision collisionInfo) {
//
//		bool left = false;
//		bool right = false;
//		if (collisionInfo.collider.tag == "Obstacle") {
//			Debug.Log (collisionInfo.contacts.Length);
//			if(collisionInfo.contacts.Length >= 2) {
//				left = true;
//				right = true;
//				foreach (ContactPoint c in collisionInfo.contacts) {
//					left = left && (c.point.x - transform.position.x < 0f);
//					right = right && (c.point.x - transform.position.x > 0f);
//				}
//			}
//		}
//		somethingOnLeft = left;
//		somethingOnRight = right;
//	}

}

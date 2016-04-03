using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DogFollow : CharacterBehavior {

	public bool touchingDirt;
	public bool inDirt;
	public List<Collider> dirtColliders;


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
		touchingDirt = false;
		inDirt = false;
		movingUp = false;
		movingDown = false;
		dirtColliders = new List<Collider> ();

	
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

		movingLeft = false;
		movingRight = false;
		movingUp = false;
		movingDown = false;

		if (!inDirt) {
			myRb.useGravity = true;
			foreach (Collider dc in dirtColliders) {
				Physics.IgnoreCollision (dc, myCollider, false);
			}
			specialMovement = false;
		}
		else {
			myRb.useGravity = false;
			foreach (Collider dc in dirtColliders) {
				Physics.IgnoreCollision (dc, myCollider, true);
			}
			specialMovement = true;
		}

		if (controlling && touchingDirt) {
			inParty = false;
			if (Input.GetKey (KeyCode.LeftArrow)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
				inParty = false;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
				inParty = false;
			}
			if (Input.GetKeyDown (KeyCode.UpArrow) && onSomething && !crouching) {
				jump = true;
				touchingDirt = false;
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				specialMovement = true;
				myRb.useGravity = false;
				foreach (Collider dc in dirtColliders) {
					Physics.IgnoreCollision (dc, myCollider, true);
				}
			}
		}

		if (controlling && !specialMovement) {
			inParty = false;
//			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
			if (Input.GetKey (KeyCode.LeftArrow)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
				inParty = false;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
				inParty = false;
			}
			if (Input.GetKeyDown (KeyCode.UpArrow) && onSomething && !crouching) {
				jump = true;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				crouching = true;
			}
			if (!Input.GetKey (KeyCode.DownArrow) && !underSomething) {
				crouching = false;
			}
		}

		if (controlling && specialMovement) {
			inParty = false;
//			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
//			myRb.velocity = new Vector3 (-1 * velocity, myRb.velocity.y, myRb.velocity.z);
//			Vector3 s = transform.localScale;
//			s.x = -1;
//			transform.localScale = s;

			if (Input.GetKey (KeyCode.LeftArrow)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
				inParty = false;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
				inParty = false;
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				movingUp = true;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				movingDown = true;
			}
		}
			
//		Debug.Log (touchingDirt);

	}


	void OnCollisionEnter(Collision collisionInfo) {
//		Debug.Log ("THIS CALLEd");
		if (controlling && collisionInfo.collider.tag == "Dirt") {
			touchingDirt = true;
			if (!dirtColliders.Contains (collisionInfo.collider)) {
				dirtColliders.Add (collisionInfo.collider);
			}
		}
	}
	void OnCollisionExit(Collision collisionInfo) {
		//		Debug.Log ("THIS CALLEd");
		if (controlling && collisionInfo.collider.tag == "Dirt") {
			touchingDirt = false;
//			if (dirtColliders.Contains (collisionInfo.collider)) {
//				dirtColliders.Remove (collisionInfo.collider);
//			}
		}
	}
		
}

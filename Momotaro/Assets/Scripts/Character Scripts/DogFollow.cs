using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DogFollow : CharacterBehavior {

	public bool touchingDirt;
	public bool inDirt;
	public List<Collider> dirtColliders;
	Quaternion defaultRotation;

	public float barkCoolDown;
	float rz;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		momo = leader.GetComponent<MomotaroBehavior> ();
		myCollider = this.gameObject.GetComponent<CapsuleCollider> ();
		fullHeight = myCollider.bounds.extents.y;
//		followInfo = followInformationObject.GetComponent<FollowInformation> ();
		infoQueue = new Queue();
		velocity = 10f;
		jumpForce = 1500f;
		myRb = GetComponent<Rigidbody> ();
		myRb.freezeRotation = true;
		inParty = true;
		specialMovement = false;
		touchingDirt = false;
		inDirt = false;
		movingUp = false;
		movingDown = false;
		dirtColliders = new List<Collider> ();
		barkCoolDown = 0f;
		defaultRotation = transform.rotation;

	
	}
	
	// Update is called once per frame
	void Update () {
		if (specialMovement) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, -2f);
		}
		else {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
		}

//		if (controlling && Vector3.Distance (leader.transform.position, transform.position) > 4f) {
//			inParty = false;
//		}
		if (barkCoolDown > 0f) {
			barkCoolDown -= Time.deltaTime;
		}
		if (!controlling && !inParty && Vector3.Distance (leader.transform.position, transform.position) < 1f) {
			transform.position = leader.transform.position;
			inParty = true;
//			infoQueue.Clear ();
		}
//		if (!controlling && Vector3.Distance (leader.transform.position, transform.position) < 0.1f) {
//			transform.position = leader.transform.position;
//			Debug.Log ("QUEUE CLEARED");
//			infoQueue.Clear ();
//		}

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
			anim.SetBool ("digging", false);
			myRb.useGravity = true;
			foreach (Collider dc in dirtColliders) {
				Physics.IgnoreCollision (dc, myCollider, false);
			}
			specialMovement = false;
		}
		else {
			myRb.useGravity = false;
			anim.SetBool ("digging", true);
			foreach (Collider dc in dirtColliders) {
				Physics.IgnoreCollision (dc, myCollider, true);
			}
			specialMovement = true;
		}

		if (controlling && touchingDirt) {
			rz = 0f;
			inParty = false;
//			crouching = false;
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
			if (Input.GetKey (KeyCode.DownArrow)) {
				myRb.useGravity = false;
				specialMovement = true;
				inDirt = true;
//				Debug.Log ("GOT HERE");
				foreach (Collider dc in dirtColliders) {
					Physics.IgnoreCollision (dc, myCollider, true);
				}
			}
			if (Input.GetKeyDown (KeyCode.Space) && barkCoolDown <= 0f) {
				anim.SetBool ("barking", true);
				bark ();
			}
			else {
				anim.SetBool ("barking", false);
			}
//			transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));

		}

		else if (controlling && !specialMovement && !touchingDirt) {
			rz = 0f;
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
			if (Input.GetKeyDown (KeyCode.Space) && barkCoolDown <= 0f) {
				anim.SetBool ("barking", true);
				bark ();
			}
			else {
				anim.SetBool ("barking", false);
			}
//			transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));

		}

		else if (controlling && specialMovement) {
			inParty = false;
			crouching = false;
//			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
//			myRb.velocity = new Vector3 (-1 * velocity, myRb.velocity.y, myRb.velocity.z);
//			Vector3 s = transform.localScale;
//			s.x = -1;
//			transform.localScale = s;
			rz = 0f;
			myRb.velocity = new Vector3 (myRb.velocity.x, 0f, 0f);
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
				specialVelocity = 5f;
				movingUp = true;
				rz = transform.localScale.x * 45f;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				specialVelocity = 5f;
				movingDown = true;
				rz = transform.localScale.x * -45f;
			}
		}
		if (!inParty && infoQueue.Count != 0) {
			infoQueue.Clear ();
		}
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, rz));

//		Debug.Log (touchingDirt);

	}


	void OnCollisionEnter(Collision collisionInfo) {
//		Debug.Log ("THIS CALLEd");
//		if (controlling && collisionInfo.collider.tag == "Dirt") {
		if (collisionInfo.collider.tag == "Dirt") {

			touchingDirt = true;
			if (!dirtColliders.Contains (collisionInfo.collider)) {
				dirtColliders.Add (collisionInfo.collider);
			}
		}
	}
	void OnCollisionExit(Collision collisionInfo) {
		//		Debug.Log ("THIS CALLEd");
//		if (controlling && collisionInfo.collider.tag == "Dirt") {
		if (collisionInfo.collider.tag == "Dirt") {
			touchingDirt = false;
//			if (dirtColliders.Contains (collisionInfo.collider)) {
//				dirtColliders.Remove (collisionInfo.collider);
//			}
		}
	}

	public void bark() {
		barkCoolDown = 0.5f;
		Collider[] enemyColliders = Physics.OverlapSphere(transform.position, 10f, 1 << LayerMask.NameToLayer ("Enemy"));
		foreach (Collider enemyCol in enemyColliders) {
			float dist = Vector3.Distance (enemyCol.transform.position, transform.position);
			EnemyBehavior enemy = enemyCol.gameObject.GetComponent<EnemyBehavior> ();
//			enemy.knockBack (((Mathf.Sign(enemyCol.transform.position.x - transform.position.x))*Vector3.right + Vector3.up * 2f).normalized * (((1f - (dist / 10f)) * 1000f)));
			enemy.knockBack (((Mathf.Sign(enemyCol.transform.position.x - transform.position.x))*Vector3.right + Vector3.up * 2f).normalized * (1000f + Random.value * 200f));

			enemy.stun (1f);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, 10f);
	}
		
}

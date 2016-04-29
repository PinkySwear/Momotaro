using UnityEngine;
using System.Collections;

public class MonkeyFollow : CharacterBehavior {

	public bool somethingOnLeft;
	public bool somethingOnRight;
	public Vector3 prevPos;
	public GameObject bananaPrefab;
	public float bananaCoolDown;
	public float bananaDelay;

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
		myRb.constraints = RigidbodyConstraints.FreezePositionZ;

		myRb.freezeRotation = true;
		inParty = true;
		specialMovement = false;
		somethingOnLeft = false;
		somethingOnRight = false;
		prevPos = transform.position;
		bananaDelay = 0f;


	}

	// Update is called once per frame
	void Update () {

		//		if (controlling && Vector3.Distance (leader.transform.position, transform.position) > 4f) {
		//			inParty = false;
		//		}

		if (bananaCoolDown > 0f) {
			bananaCoolDown -= Time.deltaTime;
		}

		if (bananaDelay > 0f) {
			bananaDelay -= Time.deltaTime;
			if (bananaDelay <= 0f) {
				throwBanana ();
			}
		}

		if (!controlling && !inParty && Vector3.Distance (leader.transform.position, transform.position) < 1.5f) {
			transform.position = leader.transform.position;
			inParty = true;
			specialMovement = false;
			anim.SetBool ("climbing", false);
			anim.speed = 1f;
//			infoQueue.Clear ();
		}

//		if (myRb.velocity.magnitude < 0.05f && momo.myRb.velocity.magnitude < 0.05f && inParty) {
//			transform.position = leader.transform.position;
//		}
//		if (!controlling && Vector3.Distance (leader.transform.position, transform.position) < 0.1f) {
//			transform.position = leader.transform.position;
//			Debug.Log ("QUEUE CLEARED");
//			infoQueue.Clear ();
//		}
	

		onSomething = underNum > 0;

	

		movingUp = false;
		movingDown = false;
		movingLeft = false;
		movingRight = false;


		myRb.useGravity = true;

		if (controlling && !specialMovement) {
			anim.SetBool ("climbing", false);
			anim.speed = 1f;
			//			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
			if (Input.GetKey(KeyCode.LeftArrow)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
				inParty = false;
//				if (myRb.velocity.x > -0.01f) {
//					somethingOnLeft = true;
//				}
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
				inParty = false;
//				if (myRb.velocity.x * -1 > -0.01f) {
//					somethingOnRight = true;
//				}
			}

			if (Input.GetKeyDown(KeyCode.UpArrow) && onSomething) {
				jump = true;
			}


			if (Input.GetKeyDown (KeyCode.Space) && bananaCoolDown <= 0f && bananaDelay <= 0f) {
				anim.SetBool ("throwing", true);
				bananaDelay = 0.3f;
			}
			else {
				anim.SetBool ("throwing", false);
			}
			if ((somethingOnLeft && movingLeft) || (somethingOnRight && movingRight)) {
				anim.SetBool ("climbing", true);

				specialMovement = true;
				//			Debug.Log ("fucking bullshit");
			}
			else {
				specialMovement = false;
			}

		}
		if (controlling && specialMovement) {
			anim.SetBool ("climbing", true);
			myRb.velocity = new Vector3 (myRb.velocity.x, 0f, 0f);	
//			specialMovement = false;
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
//			else {
//				specialMovement = false;
//			}
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
//			else {
//				specialMovement = false;
//			}
			anim.speed = 0f;
			if (Input.GetKey(KeyCode.UpArrow)) {
				anim.speed = 1f;
				specialVelocity = 5f;
				movingUp = true;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				anim.speed = 1f;
				specialVelocity = 5f;
				movingDown = true;
			}
//			somethingOnLeft = false;
//			somethingOnRight = false;
		}
//		somethingOnLeft = false;
//		somethingOnRight = false;
		if ((somethingOnLeft && movingLeft) || (somethingOnRight && movingRight)) {
			anim.SetBool ("climbing", true);

			specialMovement = true;
//			Debug.Log ("fucking bullshit");
		}
		else {
			specialMovement = false;
		}
//		somethingOnRight = false;
//		somethingOnLeft = false;
		if (!inParty && infoQueue.Count != 0) {
			infoQueue.Clear ();
		}
		somethingOnLeft = false;
		somethingOnRight = false;

	}
		
	public void throwBanana () {
		bananaCoolDown = 0.5f;
		GameObject spawnedBanana = (GameObject) Instantiate (bananaPrefab, transform.position, Quaternion.identity);
		Rigidbody bananaRB = spawnedBanana.GetComponent<Rigidbody> ();
//		bananaRB.AddForce (new Vector3 (((500f * transform.localScale.x) + (400f * (myRb.velocity.x / 10f))), 750f, 0f));
//		bananaRB.velocity = myRb.velocity;
		bananaRB.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
//		bananaRB.AddForce (Vector3.up * 1000f);
		bananaRB.AddForce ((Mathf.Sign(transform.localScale.x) * Vector3.right + Vector3.up).normalized * 1000f);
		bananaRB.AddTorque (new Vector3 (0f, 0f, ((Random.value - 0.5f) * 200f)));
	}

	void OnCollisionStay(Collision collisionInfo) {
		if (!specialMovement) {
			float minY = 100f;
			float maxY = -100f;
			bool left = false;
			bool right = false;
			if (collisionInfo.collider.tag == "Obstacle") {
//				Debug.Log (collisionInfo.contacts.Length);
				if (collisionInfo.contacts.Length >= 2) {
					left = true;
					right = true;
					foreach (ContactPoint c in collisionInfo.contacts) {
						if (c.point.y < minY)
							minY = c.point.y;
						if (c.point.y > maxY)
							maxY = c.point.y;
						left = left && (c.point.x - transform.position.x < 0f);
						right = right && (c.point.x - transform.position.x > 0f);
					}
					float minRY = transform.position.y - 0.25f;
					float maxRY = transform.position.y + 0.25f;
					bool overlap = (minY > minRY && minY < maxRY) || (maxY > minRY && maxY < maxRY) || (maxRY < maxY && minRY > minY);
					left = left && overlap;
					right = right && overlap;
				}
			}
			somethingOnLeft = left;
			somethingOnRight = right;
//			if (somethingOnLeft || somethingOnRight) {
//				specialMovement = true;
//			}
		}
	}

}

using UnityEngine;
using System.Collections;

public class PheasantFollow : CharacterBehavior {

	public GameObject poopPrefab;
	private float poopCoolDown;
	private float poopDelay;

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


	}

	// Update is called once per frame
	void Update () {

		if (poopCoolDown > 0f) {
			poopCoolDown -= Time.deltaTime;
		}

		if (poopDelay > 0f) {
			poopDelay -= Time.deltaTime;
			if (poopDelay <= 0f) {
				poop ();
			}
		}

		//		if (controlling && Vector3.Distance (leader.transform.position, transform.position) > 4f) {
		//			inParty = false;
		//		}

		if (!controlling && !inParty && Vector3.Distance (leader.transform.position, transform.position) < 1f) {
			transform.position = leader.transform.position;
			specialMovement = false;
			myRb.useGravity = true;

			inParty = true;
//			infoQueue.Clear ();
		}
//		if (!controlling && Vector3.Distance (leader.transform.position, transform.position) < 0.1f) {
//			transform.position = leader.transform.position;
//			Debug.Log ("QUEUE CLEARED");
//			infoQueue.Clear ();
//		}

		onSomething = underNum > 0;


		movingLeft = false;
		movingRight = false;
		movingUp = false;
		movingDown = false;
			
		specialMovement = false;

		if (controlling && !onSomething){
			specialMovement = true;
		}

		if (controlling && !specialMovement) {
			//			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
			if (Input.GetKey(KeyCode.LeftArrow)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
				inParty = false;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
				inParty = false;
			}

			if (Input.GetKeyDown(KeyCode.UpArrow) && onSomething) {
				jump = true;
//				specialMovement = true;
//				movingUp = true;
			}
			if (Input.GetKeyDown (KeyCode.Space) && poopCoolDown <= 0f && poopDelay <= 0f) {
				anim.SetBool ("pooping", true);
				poopDelay = 0.1f;
			}
			else {
				anim.SetBool ("pooping", false);
			}
		}

		if (controlling && specialMovement) {
			//			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
			myRb.useGravity = false;
			myRb.velocity = new Vector3 (myRb.velocity.x, 0f, 0f);
			anim.SetBool ("walking", false);
			if (Input.GetKey(KeyCode.LeftArrow)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
				inParty = false;
				myRb.velocity = new Vector3 (myRb.velocity.x, 0f, 0f);
				anim.SetBool ("walking", true);
//				myRb.AddForce (Vector3.up * 50f);
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
				inParty = false;
				myRb.velocity = new Vector3 (myRb.velocity.x, 0f, 0f);
				anim.SetBool ("walking", true);
//				myRb.AddForce (Vector3.up * 50f);
			}

			if (Input.GetKey(KeyCode.UpArrow)) {
				specialVelocity = 10f;
				movingUp = true;
				anim.SetBool ("walking", true);
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				specialVelocity = 10f;
				movingDown = true;
				anim.SetBool ("walking", true);
			}

			if (Input.GetKeyDown (KeyCode.Space) && poopCoolDown <= 0f && poopDelay <= 0f) {
				anim.SetBool ("pooping", true);
				poopDelay = 0.1f;
			}
			else {
				anim.SetBool ("pooping", false);
			}
		}
		if (!inParty && infoQueue.Count != 0) {
			infoQueue.Clear ();
		}


	}

	public void poop () {
		poopCoolDown = 0.5f;
		GameObject spawnedPoop = (GameObject) Instantiate (poopPrefab, transform.position + Mathf.Sign (transform.localScale.x) * Vector3.left * 0.3f + Vector3.down * 0.8f, Quaternion.identity);
		Rigidbody poopRB = spawnedPoop.GetComponent<Rigidbody> ();
		//		bananaRB.AddForce (new Vector3 (((500f * transform.localScale.x) + (400f * (myRb.velocity.x / 10f))), 750f, 0f));
		//		bananaRB.velocity = myRb.velocity;
		poopRB.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;

	}

}

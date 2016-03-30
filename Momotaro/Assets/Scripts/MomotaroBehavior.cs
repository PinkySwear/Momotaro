using UnityEngine;
using System.Collections;

public class MomotaroBehavior : MonoBehaviour {


	public float velocity;
	public float jumpForce;
	private Rigidbody myRb;
	private bool onSomething = false;
	private bool underSomething = false;
	private bool movingLeft;
	private bool movingRight;
	private bool jump = false;
	private bool crouching = false;

	public bool controlling;

	public GameObject followInformationObject;
	public FollowInformation followInfo;

	public GameObject[] followerObjects;
	public DogFollow[] followers;

	// Use this for initialization
	void Start () {
		followInfo = followInformationObject.GetComponent<FollowInformation> ();

		followers = new DogFollow[followerObjects.Length];
		for (int i = 0; i < followerObjects.Length; i++) {
			followers [i] = followerObjects [i].GetComponent<DogFollow> ();
			followers [i].commandDelay = 6 * (i + 1) + i;
		}


		velocity = 10f;
		jumpForce = 1000f;
		myRb = GetComponent<Rigidbody> ();
		myRb.freezeRotation = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Interactable"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));

	}

	void Update() {
		Vector3 right = transform.position + Vector3.right * transform.lossyScale.x * 0.5f;
		Vector3 left = transform.position - Vector3.right * transform.lossyScale.x * 0.5f;

		Debug.DrawLine (right, right + (Vector3.down * transform.lossyScale.y * 1));
		Debug.DrawLine (left, left + (Vector3.down * transform.lossyScale.y * 1));
		Debug.DrawLine (right, right + (Vector3.up * 1));
		Debug.DrawLine (left, left + (Vector3.up * 1));

		onSomething = Physics.Linecast (right, right + (Vector3.down * transform.lossyScale.y * 1), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.down * transform.lossyScale.y * 1), 1 << LayerMask.NameToLayer ("Obstacle"));

		underSomething = Physics.Linecast (right, right + (Vector3.up * 1), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.up * 1), 1 << LayerMask.NameToLayer ("Obstacle"));

		movingLeft = false;
		movingRight = false;
		if (controlling) {
			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
			if (Input.GetKey (KeyCode.A)) {
				movingLeft = true;
				Vector3 s = transform.localScale;
				s.x = -1;
				transform.localScale = s;
			}
			if (Input.GetKey (KeyCode.D)) {
				movingRight = true;
				Vector3 s = transform.localScale;
				s.x = 1;
				transform.localScale = s;
			}



			if (Input.GetKeyDown (KeyCode.Space) && onSomething && !crouching) {
				jump = true;
			}
			if (Input.GetKey (KeyCode.S)) {
				crouching = true;
			}
			if (!Input.GetKey (KeyCode.S) && !underSomething) {
				crouching = false;
			}
		}



	}

	// Update is called once per frame
	void FixedUpdate () {

//		FollowInformation.MovementInfo mi = new FollowInformation.MovementInfo(movingRight, movingLeft, jump, crouching);
//		FollowInformation.Tuple<int, int> t = new FollowInformation.Tuple<int, int> (Mathf.RoundToInt(transform.position.x * 100f), Mathf.RoundToInt(transform.position.y * 100f));
//		followInfo.infoMap.Remove (t);
//		followInfo.infoMap.Add (t, mi);


//		if (movingLeft || movingRight) {
//			FollowInformation.MovementInfo mi = new FollowInformation.MovementInfo(movingRight, movingLeft, jump, crouching);
//			followInfo.infoQueue.Enqueue (mi);
//		}
//		if (movingLeft || movingRight || jump || crouching || (!onSomething && (movingLeft || movingRight))) {
//			FollowInformation.MovementInfo mi = new FollowInformation.MovementInfo(movingRight, movingLeft, jump, crouching);
//			foreach (DogFollow df in followers) {
//				df.infoQueue.Enqueue (mi);
//			}
//		}



//		if (controlling) {
			FollowInformation.MovementInfo mi = new FollowInformation.MovementInfo (movingRight, movingLeft, jump, crouching);
			foreach (DogFollow df in followers) {
				df.infoQueue.Enqueue (mi);
			}
//		}
//		followInfo.infoQueue.Enqueue (mi);


		if (movingLeft) {
			//restrict movement to one plane
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			myRb.velocity = new Vector3 (-1 * velocity, myRb.velocity.y, myRb.velocity.z);
		}
		if (movingRight) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			myRb.velocity = new Vector3 (velocity, myRb.velocity.y, myRb.velocity.z);
		}

		if (crouching) {
			transform.localScale = new Vector3 (transform.localScale.x, 0.5f, 1f);
			velocity = 5f;
		}
		else {
			transform.localScale = new Vector3 (transform.localScale.x, 1f, 1f);
			velocity = 10f;
		}

		if (jump && onSomething) {
			myRb.AddForce (Vector3.up * jumpForce);
			jump = false;
		}
		if (!movingRight && !movingLeft) {
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
		transform.rotation = Quaternion.Euler (Vector3.zero);
	}
}
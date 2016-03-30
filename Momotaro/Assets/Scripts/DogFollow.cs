using UnityEngine;
using System.Collections;

public class DogFollow : MonoBehaviour {
	
	public GameObject leader;
	public MomotaroBehavior momo; 


	public GameObject followInformationObject;
	public FollowInformation followInfo;



	public float velocity;
	public float jumpForce;
	private Rigidbody myRb;
	private bool onSomething = false;
	private bool underSomething = false;
	private bool movingLeft;
	private bool movingRight;
	private bool jump = false;
	private bool crouching = false;

	public int commandDelay;

	public Queue infoQueue;


	// Use this for initialization
	void Start () {
		momo = leader.GetComponent<MomotaroBehavior> ();
//		followInfo = followInformationObject.GetComponent<FollowInformation> ();
		infoQueue = new Queue();
		velocity = 10f;
		jumpForce = 1000f;
		myRb = GetComponent<Rigidbody> ();
		myRb.freezeRotation = true;

	
	}
	
	// Update is called once per frame
	void Update () {

//		FollowInformation.Tuple<int, int> t = new FollowInformation.Tuple<int, int> (Mathf.RoundToInt(transform.position.x * 100f), Mathf.RoundToInt(transform.position.y * 100f));
//
//		if (followInfo.infoMap.ContainsKey (t)) {
//			FollowInformation.MovementInfo mi;
//			if (followInfo.infoMap.TryGetValue (t, out mi)) {
//				movingLeft = mi.movingLeft;
//				movingRight = mi.movingRight;
//				jump = mi.jump;
//				crouching = mi.crouching;
//			}
//		}
//		else {
//			movingLeft = false;
//			movingRight = false;
//			jump = false;
//			crouching = false;
//		}


	}



	void FixedUpdate () {

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

		if (jump) {
			myRb.AddForce (Vector3.up * jumpForce);
			jump = false;
		}
		if (!movingRight && !movingLeft) {
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
		transform.rotation = Quaternion.Euler (Vector3.zero);
	}
}

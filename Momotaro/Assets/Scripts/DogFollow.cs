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

	public bool controlling;
	public bool inParty;

	public int commandDelay;

	public Queue infoQueue;



	public CapsuleCollider myCollider;

	private float fullHeight;

	private Vector3 right;
	private Vector3 left;


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
//		Vector3 right = transform.position + Vector3.right * transform.lossyScale.x * 0.5f;
//		Vector3 left = transform.position - Vector3.right * transform.lossyScale.x * 0.5f;
//
//		Debug.DrawLine (right, right + (Vector3.down * transform.lossyScale.y * 1));
//		Debug.DrawLine (left, left + (Vector3.down * transform.lossyScale.y * 1));
//		Debug.DrawLine (right, right + (Vector3.up * 1));
//		Debug.DrawLine (left, left + (Vector3.up * 1));
//
//		onSomething = Physics.Linecast (right, right + (Vector3.down * transform.lossyScale.y * 1), 1 << LayerMask.NameToLayer ("Obstacle")) 
//			|| Physics.Linecast (left, left + (Vector3.down * transform.lossyScale.y * 1), 1 << LayerMask.NameToLayer ("Obstacle"));
//
//		underSomething = Physics.Linecast (right, right + (Vector3.up * 1), 1 << LayerMask.NameToLayer ("Obstacle")) 
//			|| Physics.Linecast (left, left + (Vector3.up * 1), 1 << LayerMask.NameToLayer ("Obstacle"));
		Vector3 colliderCenter = myCollider.bounds.center;
		Vector3 right = colliderCenter + Vector3.right * myCollider.bounds.extents.x;
		Vector3 left = colliderCenter - Vector3.right * myCollider.bounds.extents.x;

		Debug.DrawLine (right, right + (Vector3.down * myCollider.bounds.extents.y * 1.01f));
		Debug.DrawLine (left, left + (Vector3.down * myCollider.bounds.extents.y * 1.01f));
		Debug.DrawLine (right, right + (Vector3.up * fullHeight * 1.5f));
		Debug.DrawLine (left, left + (Vector3.up * fullHeight * 1.5f));

		onSomething = Physics.Linecast (right, right + (Vector3.down * myCollider.bounds.extents.y * 1.01f), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.down * myCollider.bounds.extents.y * 1.01f), 1 << LayerMask.NameToLayer ("Obstacle"));

		underSomething = Physics.Linecast (right, right + (Vector3.up * fullHeight * 1.5f), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.up * fullHeight * 1.5f), 1 << LayerMask.NameToLayer ("Obstacle"));

		movingLeft = false;
		movingRight = false;
		if (controlling) {
			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
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



			if (Input.GetKey(KeyCode.UpArrow) && onSomething && !crouching) {
				jump = true;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				crouching = true;
			}
			if (!Input.GetKey(KeyCode.DownArrow) && !underSomething) {
				crouching = false;
			}
		}

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

//		Vector3 right = transform.position + Vector3.right * transform.lossyScale.x * 0.5f;
//		Vector3 left = transform.position - Vector3.right * transform.lossyScale.x * 0.5f;
//		onSomething = Physics.Linecast (right, right + (Vector3.down * transform.lossyScale.y * 1), 1 << LayerMask.NameToLayer ("Obstacle")) 
//			|| Physics.Linecast (left, left + (Vector3.down * transform.lossyScale.y * 1), 1 << LayerMask.NameToLayer ("Obstacle"));

	}



	void FixedUpdate () {

		if (!controlling  && inParty) {
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

		if (jump) {
			myRb.AddForce (Vector3.up * jumpForce);
			jump = false;
		}
		if (!movingRight && !movingLeft) {
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
		//transform.rotation = leader.GetComponent<Transform>().rotation;
		//transform.rotation = Quaternion.Euler (Vector3.zero);
	}
}

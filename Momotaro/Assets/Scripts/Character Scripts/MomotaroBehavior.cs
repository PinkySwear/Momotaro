using UnityEngine;
using System.Collections;

public class MomotaroBehavior : MonoBehaviour {


	public float velocity;
	public float jumpForce;
	private Rigidbody myRb;
	private bool onSomething = false;
	private bool underSomething = false;
	public bool movingLeft;
	public bool movingRight;
	public bool jump = false;
	private bool crouching = false;

	public bool controlling;
	public bool stop;
	
	public GameObject followInformationObject;
	public FollowInformation followInfo;

	public GameObject[] followerObjects;
	public CharacterBehavior[] followers;

	public CapsuleCollider myCollider;

	private float fullHeight;

	private Vector3 right;
	private Vector3 left;

	// Use this for initialization
	void Start () {
		followInfo = followInformationObject.GetComponent<FollowInformation> ();
		myCollider = this.gameObject.GetComponent<CapsuleCollider> ();
		fullHeight = myCollider.bounds.extents.y;
		followers = new CharacterBehavior[followerObjects.Length];
		for (int i = 0; i < followerObjects.Length; i++) {
			followers [i] = followerObjects [i].GetComponent<CharacterBehavior> ();
			followers [i].commandDelay = 10 * (i + 1) + i;
		}
//		for (int i = 0; i < followerObjects.Length; i++) {
//			followers [i].commandDelay = 7 * (i + 1) + i;
//		}
//		followers [0] = followerObjects [0].GetComponent<DogFollow> ();
//		followers [1] = followerObjects [1].GetComponent<MonkeyFollow> ();
//		followers [2] = followerObjects [2].GetComponent<PheasantFollow> ();


		velocity = 10f;
		jumpForce = 1000f;
		myRb = GetComponent<Rigidbody> ();
		myRb.freezeRotation = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Interactable"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));

	}

	void Update() {
		Vector3 colliderCenter = myCollider.bounds.center;
		Vector3 right = colliderCenter + Vector3.right * myCollider.bounds.extents.x * 0.95f;
		Vector3 left = colliderCenter - Vector3.right * myCollider.bounds.extents.x * 0.95f;

		Debug.DrawLine (right, right + (Vector3.down * myCollider.bounds.extents.y * 1.001f));
		Debug.DrawLine (left, left + (Vector3.down * myCollider.bounds.extents.y * 1.001f));
		Debug.DrawLine (right, right + (Vector3.up * fullHeight * 1.5f));
		Debug.DrawLine (left, left + (Vector3.up * fullHeight * 1.5f));

		onSomething = Physics.Linecast (right, right + (Vector3.down * myCollider.bounds.extents.y * 1.001f), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.down * myCollider.bounds.extents.y * 1.001f), 1 << LayerMask.NameToLayer ("Obstacle"));

		underSomething = Physics.Linecast (right, right + (Vector3.up * fullHeight * 1.5f), 1 << LayerMask.NameToLayer ("Obstacle")) 
			|| Physics.Linecast (left, left + (Vector3.up * fullHeight * 1.5f), 1 << LayerMask.NameToLayer ("Obstacle"));

		movingLeft = false;
		movingRight = false;
//		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y + 3f, -10f);
		if (controlling && !stop) {
//			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
			if (Input.GetKey(KeyCode.LeftArrow)) {
				movingLeft = true;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				movingRight = true;
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
		else{
			// Nothing!
		}


	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
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
			foreach (CharacterBehavior df in followers) {
				if (df.inParty) {
					df.infoQueue.Enqueue (mi);
				}
			}
//		}
//		followInfo.infoQueue.Enqueue (mi);


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
			myRb.AddForce (Vector3.up * jumpForce);
			jump = false;
		}
		if (!movingRight && !movingLeft) {
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
		//transform.rotation = Quaternion.Euler (Vector3.zero);
	}
}
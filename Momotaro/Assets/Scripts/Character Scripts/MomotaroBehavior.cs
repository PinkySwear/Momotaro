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
//	private bool crouching = false;

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

	public float health;
	private float attackCoolDown;

	Animator anim;

	private Vector3 prevLoc;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		health = 6f;
		attackCoolDown = 0f;
		followInfo = followInformationObject.GetComponent<FollowInformation> ();
		myCollider = this.gameObject.GetComponent<CapsuleCollider> ();
		fullHeight = myCollider.bounds.extents.y;
		followers = new CharacterBehavior[followerObjects.Length];
		for (int i = 0; i < followerObjects.Length; i++) {
			followers [i] = followerObjects [i].GetComponent<CharacterBehavior> ();
			followers [i].commandDelay = 10 * (i + 1) + i;
		}


		velocity = 10f;
		jumpForce = 1500f;
		myRb = GetComponent<Rigidbody> ();
		myRb.freezeRotation = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Interactable"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Interactable"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Detector"), LayerMask.NameToLayer("Interactable"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Detector"), LayerMask.NameToLayer("Detector"));
	}

	void Update() {
		if (attackCoolDown > 0f) {
			attackCoolDown -= Time.deltaTime;
		}
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

//
//		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("momoAttack")) {
//			anim.SetBool ("attacking", true);
//		}
		bool inAttackState = anim.GetCurrentAnimatorStateInfo (0).IsName ("momoAttack");
//		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y + 3f, -10f);
		if (controlling && !stop) {
//			Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
			if (Input.GetKey(KeyCode.LeftArrow) && (!inAttackState || !onSomething)) {
				movingLeft = true;
			}
			if (Input.GetKey(KeyCode.RightArrow) && (!inAttackState || !onSomething)) {
				movingRight = true;
			}

//			if (Input.GetKeyDown(KeyCode.UpArrow) && onSomething && !crouching && !inAttackState) {
			if (Input.GetKeyDown(KeyCode.UpArrow) && onSomething && !inAttackState) {
				jump = true;
			}
//			if (Input.GetKey(KeyCode.DownArrow) && !inAttackState) {
//				crouching = true;
//			}
//			if (!Input.GetKey(KeyCode.DownArrow) && !underSomething) {
//				crouching = false;
//			}
			if (Input.GetKeyDown (KeyCode.Space) && attackCoolDown <= 0f) {
				anim.SetBool ("attacking", true);
				attack ();
			}
			else {
				anim.SetBool ("attacking", false);
			}
		}
		else{
			// Nothing!
		}


	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);



//		if (Mathf.Abs(prevLoc.x - transform.position.x) > 0.001f || jump) {
//			FollowInformation.MovementInfo mi = new FollowInformation.MovementInfo (movingRight, movingLeft, jump, crouching);
			FollowInformation.MovementInfo mi = new FollowInformation.MovementInfo (movingRight, movingLeft, jump);

			foreach (CharacterBehavior df in followers) {
				if (df.inParty) {
					df.infoQueue.Enqueue (mi);
				}
			}
//		}
//		followInfo.infoQueue.Enqueue (mi);
		prevLoc = transform.position;

		if (movingLeft) {
			//restrict movement to one plane
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			myRb.velocity = new Vector3 (-1 * velocity, myRb.velocity.y, myRb.velocity.z);
			Vector3 s = transform.localScale;
			s.x = -1;
			transform.localScale = s;
			anim.SetBool ("walking", true);
			anim.SetBool ("idling", false);
		}
		if (movingRight) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			myRb.velocity = new Vector3 (velocity, myRb.velocity.y, myRb.velocity.z);
			Vector3 s = transform.localScale;
			s.x = 1;
			transform.localScale = s;
			anim.SetBool ("walking", true);
			anim.SetBool ("idling", false);
		}

//		if (crouching) {
//			transform.localScale = new Vector3 (transform.localScale.x, 0.5f, 1f);
//			velocity = 5f;
//		}
//		else {
//			transform.localScale = new Vector3 (transform.localScale.x, 1f, 1f);
//			velocity = 10f;
//		}
		transform.localScale = new Vector3 (transform.localScale.x, 1f, 1f);
		velocity = 10f;

		if (jump && onSomething && Mathf.Abs (myRb.velocity.y) < 0.01f) {
			anim.SetBool ("jumping", true);
			myRb.AddForce (Vector3.up * jumpForce);
			jump = false;
		}
		else {
			anim.SetBool ("jumping", false);
		}
		if (!movingRight && !movingLeft) {
			anim.SetBool ("walking", false);
			anim.SetBool ("idling", true);
			myRb.velocity = new Vector3(0f, myRb.velocity.y, myRb.velocity.z);
		}
		//transform.rotation = Quaternion.Euler (Vector3.zero);
		if (!onSomething) {
			anim.SetBool ("falling", true);
		}
		else {
			anim.SetBool ("falling", false);
		}

	}


	public void attack() {
		attackCoolDown = 0.5f;
		Collider[] enemyColliders = Physics.OverlapSphere(transform.position, 2f, 1 << LayerMask.NameToLayer ("Enemy"));
		foreach (Collider enemyCol in enemyColliders) {
			EnemyBehavior enemy = enemyCol.gameObject.GetComponent<EnemyBehavior> ();
			enemy.takeDamage (1);
			enemy.knockBack (((Mathf.Sign(enemyCol.transform.position.x - transform.position.x)) * Vector3.right + Vector3.up * 2f).normalized * 500f);
		}
	}
}
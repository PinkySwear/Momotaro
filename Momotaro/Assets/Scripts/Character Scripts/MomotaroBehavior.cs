using UnityEngine;
using System.Collections;

public class MomotaroBehavior : MonoBehaviour {

	public bool noSword;

	public float velocity;
	public float jumpForce;
	private Rigidbody myRb;
	public bool onSomething = false;
//	private bool underSomething = false;
	public bool movingLeft;
	public bool movingRight;
	public bool jump = false;
	public int underNum;
//	private bool crouching = false;
	public bool justAttacked;

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

	public int health;
	private float attackCoolDown;
	public float attackDelay;
	public bool isDead;

	public float invuln;

	public Animator anim;

	private Vector3 prevLoc;

	public GameObject[] healthNums;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		health = 6;
		isDead = false;
		attackCoolDown = 0f;
		followInfo = followInformationObject.GetComponent<FollowInformation> ();
		myCollider = this.gameObject.GetComponent<CapsuleCollider> ();
		fullHeight = myCollider.bounds.extents.y;
		followers = new CharacterBehavior[followerObjects.Length];
		for (int i = 0; i < followerObjects.Length; i++) {
			followers [i] = followerObjects [i].GetComponent<CharacterBehavior> ();
			followers [i].commandDelay = 15 * (i + 1) + i;
		}
		foreach (GameObject g in healthNums) {
			g.SetActive (false);
		}

		healthNums [health].SetActive (true);

		velocity = 10f;
		jumpForce = 1500f;
		myRb = GetComponent<Rigidbody> ();
		myRb.constraints = RigidbodyConstraints.FreezePositionZ;

		myRb.freezeRotation = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Interactable"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));
//		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PlayerDetector"));
//		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerDetector"), LayerMask.NameToLayer("PlayerDetector"));


		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"));
//		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("PlayerDetector"));

		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Interactable"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Detector"), LayerMask.NameToLayer("Interactable"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Detector"), LayerMask.NameToLayer("Detector"));
//		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Detector"), LayerMask.NameToLayer("PlayerDetector"));



	}

	void Update() {
		if (invuln > 0f) {
			invuln -= Time.deltaTime;
		}
		if (attackCoolDown > 0f) {
			attackCoolDown -= Time.deltaTime;
		}
		if (attackDelay > 0f) {
			attackDelay -= Time.deltaTime;
			if (attackDelay <= 0f) {
				attack ();
			}
		}

		onSomething = underNum > 0;


		movingLeft = false;
		movingRight = false;

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("momoHurt")) {
			anim.SetBool ("gettingHit", false);
			return;
		}
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
			if (Input.GetKeyDown (KeyCode.Space) && attackDelay <= 0f && attackCoolDown <= 0f && !noSword) {
				anim.SetBool ("attacking", true);
				justAttacked = true;
				attackDelay = 0.2f;
			}
			else {
				anim.SetBool ("attacking", false);
				justAttacked = false;
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

		if (jump && onSomething) {
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
		attackCoolDown = 0.25f;
		Collider[] enemyColliders = Physics.OverlapSphere(transform.position + Vector3.right * Mathf.Sign(transform.localScale.x) * 1f, 2f, 1 << LayerMask.NameToLayer ("Enemy"));
		foreach (Collider enemyCol in enemyColliders) {
			EnemyBehavior enemy = enemyCol.gameObject.GetComponent<EnemyBehavior> ();
			enemy.takeDamage (1);
			enemy.stun (0.5f);
			enemy.knockBack (((Mathf.Sign(enemyCol.transform.position.x - transform.position.x)) * Vector3.right + Vector3.up * 2f).normalized * 500f);
		}
	}

	public void takeDamage (int dm) {
//		anim.SetBool ("gettingHit", true);

		if (health > 0) {
			healthNums[health].SetActive(false);
			health -= dm;
			if (dm > 0) {
				anim.SetBool ("gettingHit", true);
				invuln = 1f;
			}
			if (health > 6) {
				health = 6;
			}
			healthNums[health].SetActive(true);

		}
		if (health <= 0) {
			isDead = true;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position + Vector3.right * Mathf.Sign(transform.localScale.x) * 1f, 2f);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Obstacle")
			|| other.gameObject.layer == LayerMask.NameToLayer ("Barkable")) {
			underNum++;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Obstacle")
			|| other.gameObject.layer == LayerMask.NameToLayer ("Barkable")) {
			underNum--;
		}
	}
}
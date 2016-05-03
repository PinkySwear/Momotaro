using UnityEngine;
using System.Collections;

public class SpitterBehavior: EnemyBehavior {

//	float attackCoolDown;
	bool nearPlayer;

	private float idleTime;
	private float wanderTime;

	int numThingsInTheWay;
	bool somethingInTheWay;

	public GameObject gooPrefab;

	public float spitVelocity;
	public int derpsSpawned;
	public bool frantic;


	//current enemy state 0:default, 1:chasing, 2:in combat
	int state;

	// Use this for initialization
	void Start () {

		StartP ();
		derpsSpawned = 0;
		if (health == 0) {
			health = 3;
		}
		velocity = 4f;
		jumpForce = 1000f;
		//		movingLeft = true;
		nearPlayer = false;
		state = 0;
		idleTime = 0.1f;
	}

	void Update () {
		if (frantic) {
			stunDuration = Mathf.Min (stunDuration, 0.5f);
//			attackCoolDown = Mathf.Min (attackCoolDown, 2f);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("spitterHurt")) {
			anim.SetBool ("hurt", false);
			anim.SetBool ("walking", false);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("spitterKnockBack")) {
			anim.SetBool ("knockback", false);
			anim.SetBool ("walking", false);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("spitterPoopedOn")) {
			anim.SetBool ("poopedOn", false);
			anim.SetBool ("knockback", false);
			anim.SetBool ("walking", false);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("spitterSlip")) {
			anim.SetBool ("slip", false);
			anim.SetBool ("knockback", false);
			anim.SetBool ("walking", false);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("spitterAttack")) {
			anim.SetBool ("attacking", false);
//			anim.SetBool ("walking", false);

		}

		if (attackCoolDown > 0f) {
			attackCoolDown -= Time.deltaTime;
		}
		if (idleTime > 0f) {
			idleTime -= Time.deltaTime;
			if (idleTime <= 0f) {
				wanderTime = Random.value * 10f + 2f;
				anim.SetBool ("walking", true);
			}
		}
		else if (wanderTime > 0f) {
			wanderTime -= Time.deltaTime;
			if (wanderTime <= 0f) {
				idleTime = Random.value * 5f + 1f;
				anim.SetBool ("walking", false);
			}
		}

		state = 0;

//		if (nearPlayer && !stunned && !isDead) {
//			state = 1;
//		}

		if(Vector3.Distance(transform.position, momo.transform.position) < 20f 
			&& transform.localScale.x * (momo.transform.position.x - transform.position.x) > 0f
			&& Physics.Raycast(transform.position, momo.transform.position - transform.position, 20f, 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Interactable") | 1 << LayerMask.NameToLayer("Enemy")) 
			&& !stunned && !isDead) {
			state = 1;
		}

		if (state == 0) {

			if (idleTime > 0f) {
				movingLeft = false;
				movingRight = false;
			}
			else if (wanderTime > 0f) {
				if (!movingLeft && !movingRight) {
					if (Random.value > 0.5f) {
						movingLeft = true;
						movingRight = false;
						anim.SetBool ("walking", true);
					}
					else {
						movingLeft = false;
						movingRight = true;
						anim.SetBool ("walking", true);
					}
				}
				if (numThingsInTheWay > 0) {
					movingLeft = !movingLeft;
					movingRight = !movingRight;
				}
			}
		}

		if (state == 1) {
			Vector3 s = transform.localScale;
			s.x = Mathf.Sign(momo.transform.position.x - transform.position.x) * originalXScale;
			transform.localScale = s;
			movingLeft = false;
			movingRight = false;
			anim.SetBool ("walking", false);
//			Debug.Log ("IN STATE 1");
//			Debug.Log (attackCoolDown);
//			Debug.Log (!momo.anim.GetCurrentAnimatorStateInfo (0).IsName ("momoHurt"));
//			Debug.Log (momo.invuln <= 0f);
			if (attackCoolDown <= 0f && !momo.anim.GetCurrentAnimatorStateInfo (0).IsName ("momoHurt")  && derpsSpawned < 10) {
				attack ();
			}

		}
			
		UpdateP ();

	}

	void attack () {
//		momo.takeDamage (1);
		if (frantic) {
			attackCoolDown = ((Vector3.Distance (transform.position, momo.transform.position) / 20f) * Random.value * 2f) + 0.5f;
		}
		else {
			attackCoolDown = 2f;
		}
		Debug.Log ("CoolDown: " + attackCoolDown);
		anim.SetBool ("attacking", true);
		GameObject spawnedGoo = (GameObject) Instantiate (gooPrefab, transform.position + Vector3.up * 0.4f * transform.localScale.y, Quaternion.identity);
		Rigidbody gooRB = spawnedGoo.GetComponent<Rigidbody> ();
		int attempts = 0;
//		while (!gooRB && attempts < 100) {
//			Debug.Log (attempts);
//			gooRB = spawnedGoo.GetComponent<Rigidbody> ();
//			attempts++;
//		}
		Vector3 diff = momo.transform.position - spawnedGoo.transform.position;
		spitVelocity = 2f * Mathf.Abs (diff.x) / 3f + 20f;

		float angle = 0.5f * Mathf.Asin((-50f) * Mathf.Abs(diff.x) / (spitVelocity * spitVelocity)) + Mathf.PI/2f;
		angle -= Mathf.Sign(diff.y) * (Random.value * 0.1f + 0.05f);
		gooRB.velocity = (Mathf.Sign(diff.x) * Mathf.Cos(angle) * Vector3.right + Mathf.Sin(angle) * Vector3.up).normalized * spitVelocity;

		//		bananaRB.AddForce (new Vector3 (((500f * transform.localScale.x) + (400f * (myRb.velocity.x / 10f))), 750f, 0f));
		//		bananaRB.velocity = myRb.velocity;
		GooBehavior gb = spawnedGoo.GetComponent<GooBehavior> ();

		if (gb) {
			gb.momo = this.momo;
		}
		else {
			DerpBehavior db = spawnedGoo.GetComponent<DerpBehavior> ();
			db.mother = this;
			derpsSpawned++;
			if (db) {
				db.MomoObject = this.momo.gameObject;
			}
		}
		gooRB.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;




	}

	public void decrementDerps () {
		derpsSpawned--;
	}

	void OnTriggerEnter (Collider other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Detector" || LayerMask.LayerToName (other.gameObject.layer) == "Enemy") {
			return;
		}
		if (other.tag == "Player" && !nearPlayer) {
			nearPlayer = true;
		}
		if (other.tag == "Follower") {
			return;
		}
		//		Debug.Log (other.tag);
		numThingsInTheWay++;
	}

	void OnTriggerExit (Collider other) {
		if (LayerMask.LayerToName (other.gameObject.layer) == "Detector" || LayerMask.LayerToName (other.gameObject.layer) == "Enemy") {
			return;
		}
		if (other.tag == "Player" && nearPlayer) {
			nearPlayer = false;
		}
		if (other.tag == "Follower") {
			return;
		}
		numThingsInTheWay--;	
	}
}

using UnityEngine;
using System.Collections;

public class BananaBehavior : MonoBehaviour {

	float age;
	bool slippedOn;
	public bool active;

	// Use this for initialization
	void Start () {
		age = 0f;
		slippedOn = false;
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
		age += Time.deltaTime;
		if (Vector3.Distance(Camera.main.transform.position, transform.position) > 1000f) {
			Destroy (this.gameObject);
		}
		if (slippedOn) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 10f);
		}
		else {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (active && other.tag == "Enemy") {
			EnemyBehavior enemy = other.GetComponent<EnemyBehavior> ();
			enemy.knockBack (Vector3.up * 700f);
			enemy.stun (5f);
			enemy.setAnimBool ("slip", true);
			slippedOn = true;
			this.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * 500f);
		}
	}
	void OnCollisionEnter (Collision collision) {
		active = true;
	}


}

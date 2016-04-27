using UnityEngine;
using System.Collections;

public class PoopBehavior: MonoBehaviour {

	public bool active;

	// Use this for initialization
	void Start () {
		active = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(Camera.main.transform.position, transform.position) > 1000f) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (active && other.tag == "Enemy") {
			EnemyBehavior enemy = other.GetComponent<EnemyBehavior> ();
			enemy.knockBack (Vector3.up * 100f);
			enemy.stun (5f);
			enemy.setAnimBool ("poopedOn", true);
			this.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * 500f);
			Destroy (this.gameObject);
		}
	}
	void OnCollisionEnter (Collision collision) {
		active = true;
		Destroy (this.gameObject);
	}


}

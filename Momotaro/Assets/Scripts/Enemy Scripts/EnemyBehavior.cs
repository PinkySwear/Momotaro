using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody> ().freezeRotation = true;
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
	}
}

using UnityEngine;
using System.Collections;

public class BarkObject : MonoBehaviour {

	private Rigidbody myRb;
	public float barkForce = 10f;

	// Use this for initialization
	void Start () {
		myRb = gameObject.GetComponent<Rigidbody> ();
		myRb.constraints = RigidbodyConstraints.FreezePositionZ;
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void knockBack (Vector3 force) {
		myRb.AddForce (barkForce*force);
	}
	
}

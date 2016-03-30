using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	public GameObject momoObject;
	public GameObject dogObject;
	public GameObject monkeyObject;
	public GameObject pheasantObject;

	public MomotaroBehavior momo;
	public DogFollow dog;
	public DogFollow monkey;
	public DogFollow pheasant;

	public GameObject[] characterObjects;


	// Use this for initialization
	void Start () {
		momo = momoObject.GetComponent<MomotaroBehavior> ();
		momo.controlling = true;
		dog = dogObject.GetComponent<DogFollow> ();
		dog.controlling = false;
		monkey = monkeyObject.GetComponent<DogFollow> ();
		monkey.controlling = false;
		pheasant = pheasantObject.GetComponent<DogFollow> ();
		pheasant.controlling = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			momo.controlling = true;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)){
			momo.controlling = false;
			dog.controlling = true;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3)){
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = true;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4)){
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = true;
		}
	}
}

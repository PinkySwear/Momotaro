using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	public GameObject momoObject;
	public GameObject dogObject;
	public GameObject monkeyObject;
	public GameObject pheasantObject;

	public MomotaroBehavior momo;
	public DogFollow dog;
	public MonkeyFollow monkey;
	public PheasantFollow pheasant;

	public GameObject[] characterObjects;


	// Use this for initialization
	void Start () {
		momo = momoObject.GetComponent<MomotaroBehavior> ();
		momo.controlling = true;
		dog = dogObject.GetComponent<DogFollow> ();
		dog.controlling = false;
		monkey = monkeyObject.GetComponent<MonkeyFollow> ();
		monkey.controlling = false;
		pheasant = pheasantObject.GetComponent<PheasantFollow> ();
		pheasant.controlling = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.D)) {
//			momo.controlling = true;
//			dog.controlling = false;
//			monkey.controlling = false;
//			pheasant.controlling = false;
		}
		else if (Input.GetKey (KeyCode.F)) {
			momo.controlling = false;
			dog.controlling = true;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKey (KeyCode.D)) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = true;
			pheasant.controlling = false;
		}
		else if (Input.GetKey (KeyCode.S)) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = true;
		}
		else {
			momo.controlling = true;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
	}

	void Awake() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 120;
	}
}

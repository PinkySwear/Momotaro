using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour {

	public GameObject momoObject;
	public GameObject dogObject;
	public GameObject monkeyObject;
	public GameObject pheasantObject;
	
	public bool beginScene;

	public MomotaroBehavior momo;
	public DogFollow dog;
	public MonkeyFollow monkey;
	public PheasantFollow pheasant;

	//public GameObject triangleThing;
	//public Vector3 trianglePos;
	public GameObject[] characterObjects;

	public bool holdSwitching;

	public GameObject[] topLevel;
	public GameObject[] withPheasant;
	public GameObject[] withMonkey;
	public GameObject[] withDog;

	public Texture2D pauseTex;
	public Color initialColor;
	public Color finalColor;





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
		holdSwitching = false;
		//trianglePos = momo.transform.position + Vector3.up * 2.5f;

		pauseTex = new Texture2D(1, 1);
		initialColor = new Color (0f, 0f, 0f, 0f);
		finalColor = Color.black;
		pauseTex.SetPixel(0,0,initialColor);
		pauseTex.Apply();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SceneManager.LoadScene (0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SceneManager.LoadScene (1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SceneManager.LoadScene (2);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			SceneManager.LoadScene (3);
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			SceneManager.LoadScene (4);
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			SceneManager.LoadScene (5);
		}
		if (Input.GetKeyDown (KeyCode.Alpha7)) {
			SceneManager.LoadScene (6);
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			SceneManager.LoadScene (7);
		}
		//if (momo.controlling)
			//trianglePos = momo.transform.position + Vector3.up * 2.5f;
		//else if (dog.controlling)
			//trianglePos = dog.transform.position + Vector3.up * 1.5f;
		//else if (monkey.controlling)
			//trianglePos = monkey.transform.position + Vector3.up * 1.5f;
		//else if (pheasant.controlling)
			//trianglePos = pheasant.transform.position + Vector3.up * 2f;


		if (holdSwitching) {
			characterSwitchHold ();
		}
		else {
			characterSwitchPress ();
		}
		topLevel [0].SetActive (momo.controlling);
		topLevel [1].SetActive (dog.controlling);
		topLevel [2].SetActive (monkey.controlling);
		topLevel [3].SetActive (pheasant.controlling);
		foreach (GameObject g in withPheasant) {
			g.SetActive (pheasantObject.activeInHierarchy);
		}
		foreach (GameObject g in withMonkey) {
			g.SetActive (monkeyObject.activeInHierarchy);
		}
		foreach (GameObject g in withDog) {
			g.SetActive (dogObject.activeInHierarchy);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		//triangleThing.transform.position = trianglePos;
		if (momo.timeSinceDeath > 3f) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

			}

		}
			

	}

	void Awake() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 120;
	}


//	void OnGUI () {
//		GUI.color = Color.black;
//		if (holdSwitching) {
//			GUI.Label (new Rect (500, 10, 400, 20), "SWITCH BY HOLDING");
//			GUI.Label (new Rect (500, 40, 400, 20), "SWITCH SWITCHING MODE WITH \"O\"");
//
//		}
//		else {
//			GUI.Label (new Rect (500, 10, 400, 20), "SWITCH BY PRESSING");
//			GUI.Label (new Rect (500, 40, 400, 20), "SWITCH SWITCHING MODE WITH \"O\"");
//
//		}
//	
//	}

	void characterSwitchHold () {
		Debug.Log(beginScene);
		if ((Input.GetKey (KeyCode.S)) && dogObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = true;
			dog.inParty = false;
			monkey.controlling = false;
			pheasant.controlling = false;

		}
		else if (Input.GetKey (KeyCode.A) && monkeyObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = true;
			monkey.inParty = false;
			pheasant.controlling = false;

		}
		else if (Input.GetKey (KeyCode.W) && pheasantObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = true;
			pheasant.inParty = false;

		}
		else {
			momo.controlling = true;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			holdSwitching = false;
		}
	}

	void characterSwitchPress () {
		if (Input.GetKeyDown (KeyCode.D)) {
			momo.controlling = true;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.S) && dogObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = true;
			dog.inParty = false;
			monkey.controlling = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.A) && monkeyObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = true;
			monkey.inParty = false;
			pheasant.controlling = false;
		}
		else if (Input.GetKeyDown (KeyCode.W) && pheasantObject.activeInHierarchy) {
			momo.controlling = false;
			dog.controlling = false;
			monkey.controlling = false;
			pheasant.controlling = true;
			pheasant.inParty = false;
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			holdSwitching = true;
		}
	}

	void OnGUI () {
		if (momo.isDead) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), pauseTex);
			Color nC = Color.Lerp (initialColor, finalColor, momo.timeSinceDeath / 2f);
			pauseTex.SetPixel(0,0,nC);
			pauseTex.Apply();
		}
	
	}
}

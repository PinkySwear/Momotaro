using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour {

	public GameObject focusCharacter;
	public MomotaroBehavior momo;
	public Vector3 leftPos;
	public Vector3 rightPos;
	public float limitX;
	public bool cameraRightBefore;
	public float lastDirectionSwitch;
	public Vector3 desiredCameraPosition;
	private float distanceTraveledSinceCameraChange;
	private float timeSinceDirectionSwitch;
	private Vector3 cameraPositionAtSwitch;
	public GameObject[] parallaxLayers;
	private Vector3 prevPos;


	// Use this for initialization
	void Start () {
		momo = focusCharacter.GetComponent<MomotaroBehavior> ();
		leftPos = new Vector3 (focusCharacter.transform.position.x - 5f, focusCharacter.transform.position.y + 3f, -10f);
		rightPos = new Vector3 (focusCharacter.transform.position.x + 5f, focusCharacter.transform.position.y + 3f, -10f);
		limitX = rightPos.x;
		lastDirectionSwitch = momo.transform.position.x;
		lastDirectionSwitch = focusCharacter.transform.position.x;
		cameraRightBefore = false;
		timeSinceDirectionSwitch = 0f;
		cameraPositionAtSwitch = Vector3.zero;
		desiredCameraPosition = rightPos;
		cameraRightBefore = true;
		prevPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (momo.isDead) {
			return;
		}
		leftPos = new Vector3 (focusCharacter.transform.position.x - 5f, focusCharacter.transform.position.y + 3f, -10f);
		rightPos = new Vector3 (focusCharacter.transform.position.x + 5f, focusCharacter.transform.position.y + 3f, -10f);

		if (leftPos.x < limitX) {
			leftPos.x = limitX;
		}
		if (rightPos.x < limitX) {
			rightPos.x = limitX;
		}

		if (focusCharacter.transform.localScale.x < 0) {
			distanceTraveledSinceCameraChange = Mathf.Abs (focusCharacter.transform.position.x - lastDirectionSwitch);
			if (cameraRightBefore) {
				lastDirectionSwitch = focusCharacter.transform.position.x;
				cameraRightBefore = false;
				timeSinceDirectionSwitch = 0f;
				cameraPositionAtSwitch = Vector3.zero;
			}
			else if (distanceTraveledSinceCameraChange > 5f) {
				if (cameraPositionAtSwitch == Vector3.zero) {
					cameraPositionAtSwitch = transform.position;
				}
//				desiredCameraPosition = leftPos;
				timeSinceDirectionSwitch += Time.deltaTime;
				desiredCameraPosition = Vector3.Lerp(cameraPositionAtSwitch, leftPos, timeSinceDirectionSwitch / 1f);
			}
//			transform.position = leftPos;
		}
		else if (focusCharacter.transform.localScale.x > 0) {
			distanceTraveledSinceCameraChange = Mathf.Abs (focusCharacter.transform.position.x - lastDirectionSwitch);
			if (!cameraRightBefore) {
				lastDirectionSwitch = focusCharacter.transform.position.x;
				cameraRightBefore = true;
				timeSinceDirectionSwitch = 0f;
				cameraPositionAtSwitch = Vector3.zero;
			}
			else if (distanceTraveledSinceCameraChange > 5f) {
				if (cameraPositionAtSwitch == Vector3.zero) {
					cameraPositionAtSwitch = transform.position;
				}
//				desiredCameraPosition = rightPos;
				timeSinceDirectionSwitch += Time.deltaTime;
				desiredCameraPosition = Vector3.Lerp(cameraPositionAtSwitch, rightPos, timeSinceDirectionSwitch / 1f);
			}
		}
		transform.position = desiredCameraPosition;
//		transform.position = new Vector3 (focusCharacter.transform.position.x, focusCharacter.transform.position.y + 3f, -10f);

		Vector3 diff = transform.position - prevPos;
		diff.y = 0f;
		foreach (GameObject l in parallaxLayers) {
			l.transform.position += diff;

			l.transform.position -= diff * ((120f - (l.transform.position.z)) / 120f);	
		}


		prevPos = transform.position;
	}
		
}

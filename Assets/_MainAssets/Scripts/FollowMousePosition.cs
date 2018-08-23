using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMousePosition : MonoBehaviour {

	public Camera targetCam;
	public float zDistance = 10.0f;
	
	// Update is called once per frame
	void Update () {
		transform.position = targetCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance));
	}
}

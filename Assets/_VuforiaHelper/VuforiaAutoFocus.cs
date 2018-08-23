using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaAutoFocus : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		Destroy(this);
	}

}

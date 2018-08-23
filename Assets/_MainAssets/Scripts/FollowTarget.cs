using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

	public Transform target;

	public void Update ()
	{
		transform.position = target.position;
		transform.rotation = target.rotation;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverBehavior : MonoBehaviour {

	PhotonView _pView;

	// Use this for initialization
	void Start () {
		_pView = gameObject.GetComponent<PhotonView>();

		if( _pView.isMine )
		{
			FollowTarget followScript = gameObject.AddComponent<FollowTarget>();
			followScript.target = GameObject.Find("ARCamera").transform;
		} 
		else
		{
			Destroy(this);
		}
	}

	//public void Update()
	//{
	//	if(Input.GetMouseButtonDown(0))
	//	{
	//		Vector3 hitBoardPosition = RoomManager.Instance.GetMousePointPosition();
	//		if(hitBoardPosition.y < -10.0f) // means no hit
	//		{
	//			return;
	//		}
	//		else // got it!
	//		{
	//			CreateNewPost(hitBoardPosition);
	//		}
	//	}
	//}

	void CreateNewPost (Vector3 contactPosition) {
		MasterBehavior.Instance.AddNewNote(Color.red, "TEST111" + Random.Range(0, 20000).ToString(), contactPosition);
	}
}

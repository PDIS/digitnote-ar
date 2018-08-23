using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public static RoomManager Instance;
	public Camera mainCamera;
	public GameObject arCameraObj;
	public GameObject masterCameraObj;

	public GameObject mobileUI;
	public GameObject masterUI;

	public void Awake ()
	{
		if (PhotonNetwork.isMasterClient)
		{
			PhotonNetwork.Instantiate("roomMaster", Vector3.zero, Quaternion.identity, 0);

			mainCamera = masterCameraObj.GetComponent<Camera>();
			Destroy(arCameraObj.GetComponent<AudioListener>());

			Destroy(mobileUI);
		}      
		else
		{
			PhotonNetwork.Instantiate("observer", Vector3.zero, Quaternion.identity, 0);
			arCameraObj.SetActive(true);
			arCameraObj.GetComponent<VuforiaMonoBehaviour>().enabled = true;
			arCameraObj.GetComponent<Camera>().enabled = true;

			mainCamera = arCameraObj.GetComponent<Camera>();
			Destroy(masterCameraObj);

			// init mobile ui
			mobileUI.SendMessage("UIInit");
		}
	}

	public void Start()
	{
		Instance = this;
	}

	public Vector3 GetMousePointPosition () {
		if( PhotonNetwork.isMasterClient )
		{
			return mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
		}
		else
		{
			Vector3 clickPosition = RoomManager.Instance.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100.0f));
			Ray dirRay = new Ray(arCameraObj.transform.position, (clickPosition - arCameraObj.transform.position).normalized);
            RaycastHit[] hitData = Physics.RaycastAll(dirRay, 100.0f);

            for (int i = 0; i < hitData.Length; i++)
            {
                if (hitData[i].collider.tag == "post-board")
                {
					return hitData[i].point;
                }
            }
		}

		// if no hit
		return new Vector3(-100.0f, -100.0f, -100.0f);
	}
}

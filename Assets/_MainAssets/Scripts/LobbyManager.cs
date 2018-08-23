using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class LobbyManager : MonoBehaviour {

	public GameObject mainCanvas;

	public void Awake()
	{
		PhotonNetwork.autoJoinLobby = false; // we join by user select room
		PhotonNetwork.automaticallySyncScene = true;
	}

	public IEnumerator Start()
	{
		PhotonNetwork.ConnectUsingSettings("1.0");

        // activate UI after loading done
		mainCanvas.SetActive(false);
		while(true)
		{
			if (PhotonNetwork.connected)
				break;
			else
				yield return null;
		}

		// connected!
		mainCanvas.SetActive(true);
        
	}

	public void Update()
	{
		if( Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log(PhotonNetwork.isMasterClient);
		}
	}

	public void GetRooms () {
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();

		for (int i = 0; i < rooms.Length; i++)
		{
			Debug.Log(rooms[i].Name);
			Debug.Log(rooms[i].PlayerCount);
		}

		PhotonNetwork.JoinRoom("testRoom");
	}

	public void CreateRoom () {
		PhotonNetwork.CreateRoom("testRoom");
	}

	public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public void OnPhotonCreateRoomFailed()
	{
        Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
    }

    public void OnPhotonJoinRoomFailed(object[] cause)
    {
        
        Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
    }

    public void OnPhotonRandomJoinFailed()
    {
        
        Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        PhotonNetwork.LoadLevel("demoRoom");
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected from Photon.");
    }

    public void OnFailedToConnectToPhoton(object parameters)
    {
        //this.connectFailed = true;
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("As OnConnectedToMaster() got called, the PhotonServerSetting.AutoJoinLobby must be off. Joining lobby by calling PhotonNetwork.JoinLobby().");
        PhotonNetwork.JoinLobby();
    }
}

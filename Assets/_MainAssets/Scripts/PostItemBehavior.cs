using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostItemBehavior : MonoBehaviour {

	public int noteId = 0;
	public TextMeshPro textPro;
	public MeshRenderer postBody;

    // for dragging
	bool isDraging = false;
	Vector3 posDelta = Vector3.zero;

	// is moving via server
	float timeToMove = 0.0f;
	bool isMovingByServer = false;
	Vector3 serverToPosition = Vector3.zero;

    public void SetContentText (string newText)
	{
		textPro.text = newText;
	}

    public void SetColor (Color toColor)
	{
		postBody.material.color = toColor;
	}

	public void SetOutline(Color outlineColor)
	{
		postBody.material.SetFloat("_Outline", 1.0f);
		postBody.material.SetColor("_OutlineColor", outlineColor);
	}

    public void SetLineOff ()
	{
		postBody.material.SetFloat("_Outline", 0.0f);
	}

	public void OnMouseOver()
	{
		if( PhotonNetwork.isMasterClient )
		{
			MasterBehavior.Instance.SetNoteFocusViaServer(this.noteId);
			SetOutline(PredefinedColors.NoteFocusColor);
		}
	}

	public void OnMouseExit()
	{
		if(PhotonNetwork.isMasterClient)
		{
			MasterBehavior.Instance.SetNoteOutFocusViaServer(this.noteId);
			SetLineOff();
		}
	}

	public void OnMouseDown()
	{
		isDraging = true;
		isMovingByServer = false; // interrupt moving animation

		Vector3 mousePos = RoomManager.Instance.GetMousePointPosition();
		mousePos.y = 0.0f;

		posDelta = mousePos - transform.position;
        
	}

	public void OnMouseUp()
	{
		isDraging = false;
		MasterBehavior.Instance.MoveTargetNote(this.noteId, transform.position);
	}

	public void Update()
	{
		// draging
		if(isDraging)
		{
			Vector3 toPos = RoomManager.Instance.GetMousePointPosition();
			toPos.y = 0.0f;
			toPos -= posDelta;

			transform.position = toPos;
		}
		// use else if to prevent server overrides
		else if( isMovingByServer )
		{
			transform.position = Vector3.Lerp(transform.position, serverToPosition, 0.2f);
			timeToMove -= Time.deltaTime;

			if(timeToMove< 0.0f)
			{
				isMovingByServer = false;
				transform.position = serverToPosition;
			}
		}
	}

	public void MoveToPosition (Vector3 toPosition) {
		serverToPosition = toPosition;
		timeToMove = 2.0f;
		isMovingByServer = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBehavior : MonoBehaviour {

	public static MasterBehavior Instance;
	PhotonView _pView;

	public GameObject notePrefab;
	public Dictionary<int, GameObject> postDir;
	int nowNoteId = 0;

	// Use this for initialization
	void Start () {
		Instance = this;
		_pView = gameObject.GetComponent<PhotonView>();

		postDir = new Dictionary<int, GameObject>();

		if(_pView.isMine)
		{
			FollowMousePosition followScript = gameObject.AddComponent<FollowMousePosition>();
			followScript.targetCam = GameObject.Find("MasterCamera").GetComponent<Camera>();
			followScript.zDistance = 10.0f;
		}
	}

	public void Update()
	{
		// for test
		if(Input.GetKeyDown(KeyCode.Q))
		{
			AddNewNote(Color.cyan, "DEMO123", new Vector3(Random.Range(-6.0f, 6.0f), 0.0f, 0.0f))   ;
		}
	}


	public void AddNewNote (Color noteColor, string content, Vector3 notePosition)
	{
		_pView.RPC("RPCAddNewNote", PhotonTargets.All, ColorToVector3(noteColor), content, notePosition);
	}

    [PunRPC]
	void RPCAddNewNote (Vector3 colorVector, string content, Vector3 notePosition)
	{
		GameObject noteObj = Instantiate(notePrefab, notePosition, Quaternion.identity);
		postDir.Add(nowNoteId, noteObj);

		PostItemBehavior postScript = noteObj.GetComponent<PostItemBehavior>();
		postScript.SetColor(Vector3ToColor(colorVector));
		postScript.SetContentText(content);
		postScript.noteId = nowNoteId;

		nowNoteId++;
	}

	public void MoveTargetNote (int noteId, Vector3 toPosition)
	{
		_pView.RPC("RPCMoveTargetNote", PhotonTargets.All, noteId, toPosition);
	}

	[PunRPC]
	void RPCMoveTargetNote (int noteId, Vector3 toPosition)
	{
		GameObject targetNote = postDir[noteId];

        // let itself do the animation
		targetNote.GetComponent<PostItemBehavior>().MoveToPosition(toPosition);
	}

	[PunRPC]
    void RPCEditNoteContent (int noteId, string newContent)
	{
		GameObject targetNote = postDir[noteId];
		targetNote.GetComponent<PostItemBehavior>().SetContentText(newContent);
	}

	[PunRPC]
	void RPCEditNoteColor (int noteId, Color toColor)
	{
		GameObject targetNote = postDir[noteId];
		targetNote.GetComponent<PostItemBehavior>().SetColor(toColor);
	}

	[PunRPC]
    void RPCDeleteNote (int noteId)
	{
		
	}

	Vector3 ColorToVector3 (Color color)
	{
		return new Vector3(color.r, color.g, color.b);
	}

	Color Vector3ToColor (Vector3 colorVector)
	{
		return new Color(colorVector.x, colorVector.y, colorVector.z);
	}
}

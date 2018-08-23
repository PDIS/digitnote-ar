using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAddNewNote : MonoBehaviour {

	public InputField textInput;
	public GameObject[] colorPickItems;
	public Color[] pickableColors;
	int pickedIndex = -1;

    // reset color pick items
	public void ResetUI()
	{
		textInput.text = "";
		pickedIndex = -1;

		for (int i = 0; i < colorPickItems.Length; i++)
		{
			colorPickItems[i].transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	public void ColorPickIndex (int newPickIndex)
	{
		// deactivate last picked
		if( pickedIndex != -1) // if not the first selected
			colorPickItems[pickedIndex].transform.GetChild(0).gameObject.SetActive(false);

		colorPickItems[newPickIndex].transform.GetChild(0).gameObject.SetActive(true);

		pickedIndex = newPickIndex;
	}
 
	public void SendNewNoteData () {
		Vector3 touchedPos = RoomManager.Instance.GetMousePointPosition();
		MasterBehavior.Instance.AddNewNote(pickableColors[pickedIndex], textInput.text, touchedPos);
	}
}

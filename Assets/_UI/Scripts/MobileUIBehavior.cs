using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUIBehavior : MonoBehaviour {

	public Animator mainButton;

	public void UIInit () {
		mainButton.Play("start");
	}

	public void OpenUI (Animator targetUIAnimator)
	{
		targetUIAnimator.Play("start");
	}

	public void CloseUI (Animator targetUIAnimator)
	{
		targetUIAnimator.Play("end");
	}
}

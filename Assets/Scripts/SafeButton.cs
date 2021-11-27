using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeButton : MonoBehaviour, IInteractable
{

	public Safe Safe;

	public SafeButtonType ButtonType;
	// Use this for initialization
	void Start()
	{
	
	}

	// Update is called once per frame
	void Update () {
		
	}

	public bool Interact()
	{
		Debug.Log("Button Clicked!");
		if (ButtonType == SafeButtonType.LetterC) Safe.Clear();
		else if(ButtonType==SafeButtonType.Asterisk){}
		else if (ButtonType >= SafeButtonType.Digit0 && ButtonType <= SafeButtonType.Digit9)Safe.InsertNumber((int)ButtonType);
		return true;
	}


	public enum SafeButtonType
	{
		Digit0,
		Digit1,
		Digit2,
		Digit3,
		Digit4,
		Digit5,
		Digit6,
		Digit7,
		Digit8,
		Digit9,
		LetterC,
		Asterisk
	};

}

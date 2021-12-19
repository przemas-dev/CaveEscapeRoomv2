using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadlockWheel : MonoBehaviour,IInteractable
{

	public int WheelNumber;
	public Padlock Padlock;
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Interact()
	{
		Padlock.SpinDiskUp(WheelNumber);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour, IGrabable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsGrabbed { get; set; }
	public void Grab(GameObject grabber)
	{
		throw new System.NotImplementedException();
	}

	public void Release()
	{
		throw new System.NotImplementedException();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{


	private List<GameObject> _stickedObjects = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionExit(Collision other)
	{
		_stickedObjects.Remove(other.gameObject);
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("sticky"))
        {
			_stickedObjects.Add(other.gameObject);
		}
	}

	private void OnCollisionStay(Collision other)
	{
		if (_stickedObjects.Contains(other.gameObject))
		{
			var rigidbody = other.rigidbody;
			rigidbody.useGravity = false;
			rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
			rigidbody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
		}
	}
	
}

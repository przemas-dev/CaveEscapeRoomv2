using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Locker : MonoBehaviour, IGrabable
{
	
	private Transform _grabberTransform;
	private float _grabDistance;
	public int SpeedFactor = 100;
	private Rigidbody _rigidbody;
	private Vector3 _grabPosition;
	public Transform ForcePoint;

	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.centerOfMass = transform.localPosition;
		_rigidbody.inertiaTensorRotation = Quaternion.identity;
	}
	
	void Update ()
	{
		if (IsGrabbed)
		{
			_grabPosition = _grabberTransform.position + _grabberTransform.forward * _grabDistance;
			_rigidbody.AddForceAtPosition((_grabPosition - ForcePoint.position) * SpeedFactor,
				ForcePoint.position);
		}
		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color=Color.black;
		Gizmos.DrawSphere(_grabPosition,0.05f);
		if (IsGrabbed)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(transform.position, _grabPosition);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, transform.position + Vector3.back);
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(ForcePoint.position, _grabPosition);
		}

	}

	public bool IsGrabbed { get; set; }
	
	
	public void Grab(GameObject grabber)
	{
		IsGrabbed = true;
		_grabberTransform = grabber.transform;
		_grabDistance = (transform.position - _grabberTransform.position).magnitude;
		_rigidbody.angularDrag = 10.0f;
	}

	public void Release()
	{
		IsGrabbed = false;
		_rigidbody.angularDrag = 0.05f;
	}
}

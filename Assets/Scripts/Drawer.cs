using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Drawer : MonoBehaviour, IGrabable
{

	public float MaxMove = 0.6f;
	public Axis Direction;
	private Vector3 _startingPos;
	private Vector3 _minPos;
	private Vector3 _maxPos;

	private Transform _grabberTransform;
	private Vector3 _grabPosition;
	private float _grabDistance;
	private Rigidbody _rigidbody;
	private Vector3 velocity;
	private Vector3 _directionVector;
	
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.drag = 1.0f;
		_startingPos = transform.position;
		switch (Direction)
		{
			case Axis.None:
				throw new Exception("Select the axis of movement");
				break;
			case Axis.X:
				_directionVector=Vector3.right;
				break;
			case Axis.Y:
				_directionVector=Vector3.up;
				break;
			case Axis.Z:
				_directionVector=Vector3.back;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		_maxPos = _startingPos + _directionVector * MaxMove;
		_minPos=_startingPos + _directionVector * MaxMove *(-1);
	}
	
	void Update ()
	{
		if (IsGrabbed)
		{
			_grabPosition = _grabberTransform.position + _grabberTransform.forward * _grabDistance;
			velocity = (_grabPosition - transform.position) * 10.0f;
			velocity.Scale(_directionVector);
			_rigidbody.velocity = velocity;
		}
		if ((transform.position - _startingPos).magnitude > MaxMove)
		{
			if (transform.position.magnitude < _startingPos.magnitude) transform.position = _maxPos;
			else transform.position = _minPos;
		}
	}
	
	public bool IsGrabbed { get; set; }
	public void Grab(GameObject grabber)
	{
		IsGrabbed = true;
		Debug.Log("grabbed");
		_grabberTransform = grabber.transform;
		_grabDistance = (transform.position - _grabberTransform.position).magnitude;
	}

	public void Release()
	{
		Debug.Log("released");
		IsGrabbed = false;
	}

	public void OnDrawGizmos()
	{
		if (IsGrabbed)
		{
			Gizmos.color=Color.black;
			Gizmos.DrawSphere(_grabPosition,0.05f);
		}
		
	}
}

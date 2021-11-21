using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Drawer : MonoBehaviour
{

	public float MaxMove = 0.6f;
	public Axis Direction;
	private Vector3 _startingPos;
	private Vector3 _minPos;
	private Vector3 _maxPos;
	void Start ()
	{
		_startingPos = transform.position;
		switch (Direction)
		{
			case Axis.None:
				throw new Exception("Select the axis of movement");
				break;
			case Axis.X:
				_minPos = new Vector3(_startingPos.x - MaxMove, _startingPos.y, _startingPos.z);
				_maxPos = new Vector3(_startingPos.x + MaxMove, _startingPos.y, _startingPos.z);
				break;
			case Axis.Y:
				_minPos = new Vector3(_startingPos.x, _startingPos.y - MaxMove, _startingPos.z);
				_maxPos = new Vector3(_startingPos.x, _startingPos.y + MaxMove, _startingPos.z);
				break;
			case Axis.Z:
				_minPos = new Vector3(_startingPos.x, _startingPos.y, _startingPos.z - MaxMove);
				_maxPos = new Vector3(_startingPos.x, _startingPos.y, _startingPos.z + MaxMove);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
	
	void Update ()
	{
		var diff = transform.position - _startingPos;
		if ((transform.position - _startingPos).magnitude > MaxMove)
		{
			if (transform.position.magnitude < _startingPos.magnitude) transform.position = _maxPos;
			else transform.position = _minPos;
		}
	}
}

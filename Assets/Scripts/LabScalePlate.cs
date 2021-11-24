using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabScalePlate : MonoBehaviour {

    public LabScale LabScale;

	private void OnCollisionEnter(Collision other)
	{
		LabScale.ChangeMassOnScale(other.rigidbody.mass);
	}

	private void OnCollisionExit(Collision other)
	{
		LabScale.ChangeMassOnScale(0.0f);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabScale : MonoBehaviour
{

	public TextMesh TextMesh;
	public Material MaterialScreenOn;
	public Material MaterialScreenOff;
	public MeshRenderer ScaleScreen;
	[Tooltip("Measurement time given in frames")]
	public int MeasurementTime = 20;

	private string _screenText = "0.00";
	private float _screenValue = 0.0f;
	private float _value = 0.0f;
	private int _remainingWeighingFrames = 0;
	private bool _powerOn = true;
	private NetworkView nv;

	void Start()
	{
		nv = GetComponent<NetworkView>();
	}

	public void Switch()
	{
		_powerOn = !_powerOn;
		ScaleScreen.material = _powerOn ? MaterialScreenOn : MaterialScreenOff;
		ScreenUpdate();
	}
	public void ChangeMassOnScale(float mass)
	{
		_value = mass;
		_remainingWeighingFrames = MeasurementTime;
	}

	private void ScreenUpdate()
	{
		_screenText = _screenValue.ToString("F2");
		//TextMesh.text = _powerOn ? _screenText : "";
		nv.RPC("ChangeTextRPC", RPCMode.All, _powerOn ? _screenText : "");
	}

	[RPC]
	private void ChangeTextRPC(string text)
	{
		TextMesh.text = text;
	}


	void Update()
	{

		if (_remainingWeighingFrames > 1)
		{
			_screenValue += (_value - _screenValue) * 0.3f;
			_remainingWeighingFrames--;
			ScreenUpdate();
		}
		else if (_remainingWeighingFrames == 1)
		{
			_screenValue = _value;
			_remainingWeighingFrames--;
			ScreenUpdate();
		}
	}
}

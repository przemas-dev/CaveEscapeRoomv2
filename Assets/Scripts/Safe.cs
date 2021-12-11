using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{


	public TextMesh ScreenText;
	private AudioSource _audioSource;
	public AudioClip BeepSound;
	public AudioClip AccessDeniedSound;
	public AudioClip UnlockSound;
	public GameObject SafeDoor;
	[Tooltip("Value between 0 and 9999")] 
	public int SafeCode = 1911;

	private string _screenText = "";
	private bool _isOpen = false;
	
	// Use this for initialization
	void Start ()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Awake()
	{
		SafeDoor.GetComponent<Locker>().enabled = false;
		SafeDoor.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void InsertNumber(int number)
	{
		if (_isOpen) return;
		if (_screenText.Length > 4) _screenText = "";
		Beep();
		_screenText += number.ToString();
		if (_screenText.Length == 4) CheckCode();
		UpdateScreen();
	}
	public void Clear()
	{
		if (_isOpen) return;
		Beep();
		_screenText = "";
		UpdateScreen();
	}

	private void CheckCode()
	{
		if (int.Parse(_screenText) == SafeCode) Open();
		else AccessDenied();
	}

	private void Open()
	{
		_isOpen = true;
		UnlockSafe();
		_screenText = "Open";
		UpdateScreen();
		_audioSource.clip = UnlockSound;
		_audioSource.Play();
	}

	private void UnlockSafe()
	{
		SafeDoor.GetComponent<Locker>().enabled = true;
		var constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX |
		                  RigidbodyConstraints.FreezeRotationY;
		SafeDoor.GetComponent<Rigidbody>().constraints = constraints;
	}
	
	private void UpdateScreen()
	{
		ScreenText.text = _screenText;
	}
	
	private void AccessDenied()
	{
		_audioSource.clip = AccessDeniedSound;
		_audioSource.Play();
		_screenText = "Error";
		UpdateScreen();
	}
	
	private void Beep()
	{
		_audioSource.clip = BeepSound;
		_audioSource.Play();
	}
}

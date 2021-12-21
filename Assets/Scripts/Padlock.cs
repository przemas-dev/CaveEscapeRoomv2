using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Padlock : MonoBehaviour {

	public int CipherCode;
	public bool RandomDiskStartPos = true;
	public Transform Shackle;
	public Transform[] NumberDisks = new Transform[3];
	public AudioClip[] PadlockSounds;
	
	public bool _isOpened = false;
	private int[] _digits = new int[3];
	private AudioSource _audioSource;
    private NetworkView nv;
	
	void Start()
	{
        nv = GetComponent<NetworkView>();
		_audioSource = GetComponent<AudioSource>();
		if (RandomDiskStartPos)
		{
			var r = new System.Random();
			SetDiskNumber(0, r.Next(9), false);
			SetDiskNumber(1, r.Next(9), false);
			SetDiskNumber(2, r.Next(9), false);
		}
	}
	
	private void SetDiskNumber(int diskNumber, int value, bool playSound = true)
	{
		_digits[diskNumber] = value;
        SetDiskRotation(diskNumber, value);
		if (playSound) PlaySound();
	}
	// Update is called once per frame
	void Update () {
		
	}

	private void SetDiskRotation(int diskNumber, int value)
	{
        nv.RPC("SetDiskRotationRPC", RPCMode.OthersBuffered, diskNumber, value);
        NumberDisks[diskNumber].localRotation = Quaternion.Euler(0.0f, 0.0f, value * 36.0f);
	}
    [RPC]
    private void SetDiskRotationRPC(int diskNumber, int value)
    {
        NumberDisks[diskNumber].localRotation = Quaternion.Euler(0.0f, 0.0f, value * 36.0f);
    }



    public void SpinDiskUp(int diskNumber)
	{
		if (_isOpened) return;
		_digits[diskNumber] = (_digits[diskNumber] + 1) % 10;
		SetDiskRotation(diskNumber,_digits[diskNumber]);
		PlaySound();
		//TODO: animacja 
		CheckNumbers();
	}

	public void SpinDiskDown(int diskNumber)
	{
		if (_isOpened) return;
		_digits[diskNumber] = _digits[diskNumber] == 0 ? 9 : _digits[diskNumber] - 1;
		SetDiskRotation(diskNumber,_digits[diskNumber]);
		PlaySound();
		//TODO animacja
		CheckNumbers();
	}
	private void CheckNumbers()
	{
		int code = _digits[0] * 100 + _digits[1] * 10 + _digits[2];
		if (code == CipherCode) OpenPadlock();
	}
	
	private void OpenPadlock()
	{
		var colliders = Shackle.GetComponentsInChildren<BoxCollider>();
		Debug.Log("Padlock is open!");
		foreach (var collider in colliders)
		{
			collider.enabled = false;
		}

		gameObject.AddComponent<Grabable>();
		_isOpened = true;
	}
	
	private void PlaySound()
	{
		_audioSource.clip = PadlockSounds[new Random().Next(PadlockSounds.Length - 1)];
		_audioSource.Play();
	}
}

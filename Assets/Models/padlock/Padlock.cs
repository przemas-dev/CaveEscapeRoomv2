using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Padlock : MonoBehaviour, IInteractable
{
    public int CipherCode;
    public bool RandomDiskStartPos = true;
    public Transform Shackle;
    public Transform[] NumberDisks = new Transform[3];
    public AudioClip[] PadlockSounds;

    

    private bool _isOpened = false;
    private int[] _digits = new int[3];
    private AudioSource _audioSource;
    private UIManager _uiManager;


    public string Name
    {
        get
        {
            return "Padlock";
        }
    }

    private int _inputDigitIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (RandomDiskStartPos)
        {
            var r = new System.Random();
            SetDiskNumber(0, r.Next(9), false);
            SetDiskNumber(1, r.Next(9), false);
            SetDiskNumber(2, r.Next(9), false);
        }
        _uiManager = GameObject.FindObjectOfType<UIManager>();
        _uiManager.DisablePadlockCanvas();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpinDiskUp(int diskNumber)
    {
        _digits[diskNumber] = (_digits[diskNumber]++) % 10;
        //TODO: animacja 
        CheckNumbers();
    }

    public void SpinDiskDown(int diskNumber)
    {
        _digits[diskNumber] = _digits[diskNumber] == 0 ? 9 : _digits[diskNumber] - 1;
        //TODO animacja
        CheckNumbers();
    }

    private void SetDiskNumber(int diskNumber, int value, bool playSound = true)
    {
        _digits[diskNumber] = value;
        Transform disk = NumberDisks[diskNumber];
        disk.localRotation = Quaternion.Euler(0.0f, 0.0f, value * 36.0f);
        if (playSound) PlaySound();
    }

    private void CheckNumbers()
    {
        int code = _digits[0] * 100 + _digits[1] * 10 + _digits[2];
        if (code == CipherCode) OpenPadlock();
    }

    public bool Interact()
    {
        if (_isOpened) return true;
        GameManager.GameManagerInstance.LockMovement();
        GameManager.GameManagerInstance.LockCamera();
        _uiManager.EnablePadlockCanvas();




        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiManager.DisablePadlockCanvas();
            GameManager.GameManagerInstance.UnlockMovement();
            GameManager.GameManagerInstance.UnlockCamera();
            _inputDigitIndex = 0;
            return true;
        }

        var key = InputExtension.GetKeyDown(KeyCodeType.Numbers);
        if (key != null) SetDiskNumber(_inputDigitIndex++, InputExtension.GetNumber(key));

        if (_inputDigitIndex == 3)
        {
            CheckNumbers();
            _uiManager.DisablePadlockCanvas();
            GameManager.GameManagerInstance.UnlockMovement();
            GameManager.GameManagerInstance.UnlockCamera();
            _inputDigitIndex = 0;
            return true;
        }

        return false;
    }

    private void PlaySound()
    {
        _audioSource.clip = PadlockSounds[new Random().Next(PadlockSounds.Length - 1)];
        _audioSource.Play();
    }

    private void OpenPadlock()
    {
        var colliders = Shackle.GetComponentsInChildren<BoxCollider>();
        Debug.Log(colliders.Length);
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        gameObject.AddComponent<Pickable>();
        _isOpened = true;
    }
}
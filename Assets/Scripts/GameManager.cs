using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM_instance;

    private GameObject _player;

    

    public enum GameState
    {
        GS_MAINMENU,
        GS_RUNNING,
    }

    public static GameManager GameManagerInstance { get; private set; }
    private void Awake()
    {
        if (GameManagerInstance != null && GameManagerInstance != this)
            throw new Exception("Not allowed to create another copy of GameManager class.");
        GameManagerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LockMovement()
    {
        _player.GetComponent<CharacterController>().enabled = false;
    }
    public void UnlockMovement()
    {
        _player.GetComponent<CharacterController>().enabled = true;
    }
    public void LockCamera()
    {
        _player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
    }
    public void UnlockCamera()
    {
        _player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
    }
}



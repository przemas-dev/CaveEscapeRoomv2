using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour {

    private bool win = false;

    private AudioSource _audioSource;

    public GameObject[] padlocks;

    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
        if (!win) winCondition();
    }

    private void winCondition()
    {
        int count = 0;
        foreach (var padlock in padlocks)
        {
            if (padlock.GetComponent<Padlock>()._isOpened) count++;
        }

        if (count == 5)
        {
            win = true;
            _audioSource.Play();
        }
        else count = 0;
    }
}

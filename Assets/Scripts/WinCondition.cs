using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour {

    public bool win = false;

    private AudioSource _audioSource;

    public GameObject[] padlocks;
    public ClockController _clock;

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

        if (count == padlocks.Length)
        {
            win = true;
            _audioSource.Play();
            _clock.end = true;
        }
        else count = 0;
    }
}

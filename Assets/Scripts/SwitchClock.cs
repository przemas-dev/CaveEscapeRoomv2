using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchClock : MonoBehaviour, IInteractable
{
    private ClockController _clock;
	// Use this for initialization
	void Start ()
    {
        _clock = GameObject.FindObjectOfType<ClockController>();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public bool Interact()
    {
        _clock.showCode();
        return false;
    }
}

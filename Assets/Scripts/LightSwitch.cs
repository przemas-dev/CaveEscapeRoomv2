using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Switch
{

    public GameObject switch_up;
    public GameObject switch_down;
    private AudioSource _audioSource;
    private NetworkView nv;
    private bool isOn = true;

	// Use this for initialization
	void Start ()
    {
        nv = GetComponent<NetworkView>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override bool Interact()
    {
        
        isOn = !isOn;
        _audioSource.Play();
        SetActiveObject(isOn);
        nv.RPC("SetActiveObject", RPCMode.AllBuffered, isOn);
        return base.Interact();
    }


    [RPC]
    private void SetActiveObject(bool isLightOn)
    {
        switch_up.SetActive(isLightOn);
        switch_down.SetActive(!isLightOn);
    }
}

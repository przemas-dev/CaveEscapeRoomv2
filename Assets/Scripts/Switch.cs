using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour,IInteractable {

    public GameObject[] SwitchableObjects;

    public virtual bool Interact()
    {
        foreach(var switchable in SwitchableObjects)
        {
            switchable.GetComponent<ISwitchable>().Switch();
        }
        return true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

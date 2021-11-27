using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactions : MonoBehaviour {


    public float CarrySpeed = 15.0f;
    public float InteractionRange = 2.0f;
    private PlayerState PlayerState { get; set; }

    public float CarryDistance = 1.0f;
    public GameObject FlyStickObject;
    private IGrabable _grabbedObject = null;
    private NetworkView nv;
    private GameObject _observedObject;
    
    
    // Use this for initialization
    void Start ()
    {
        nv = GetComponent<NetworkView>();
        //Lzwp.AddAfterInitializedAction(Init);
    }

    private void Init()
    {
        Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnPress += PressedFire;
        Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnRelease += ReleasedFire;

    }

    private void PressedFire()
    {
        _observedObject = GetObservedObject();
        var grabable = Grabable(_observedObject);
        var interactable = Interactable(_observedObject);
        if (interactable != null) interactable.Interact();
        if (grabable == null) return;
        grabable.Grab(FlyStickObject);
        PlayerState = PlayerState.Carrying;
        _grabbedObject = grabable;
    }

    private void ReleasedFire()
    {
        _grabbedObject.Release();
        PlayerState = PlayerState.NotOccupied;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(PlayerState==PlayerState.Carrying)
                ReleasedFire();
            else if(PlayerState==PlayerState.NotOccupied)
                PressedFire();
        }
    }

    private void Awake()
    {
        PlayerState = PlayerState.NotOccupied;
    }
    
    
    GameObject GetObservedObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(FlyStickObject.transform.position, FlyStickObject.transform.forward, out hit))
        {
            if (hit.distance < InteractionRange)
                return hit.collider.gameObject;
        }
        return null;
    }
    private IGrabable Grabable(GameObject gameObject)
    {
        var grabable = gameObject.GetComponent<IGrabable>();
        return grabable != null && ((MonoBehaviour) grabable).enabled ? grabable : null;
    }
    private IInteractable Interactable(GameObject gameObject)
    {
        var interactable = gameObject.GetComponent<IInteractable>();
        return interactable != null && ((MonoBehaviour) interactable).enabled ? interactable : null;
    }
}

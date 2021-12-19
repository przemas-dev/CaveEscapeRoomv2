using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactions : MonoBehaviour
{
    public float InteractionRange = 2.0f;
    private PlayerStateEnum PlayerState { get; set; }

    public GameObject FlyStickObject;
    private IGrabable _grabbedObject;
    private NetworkView nv;
    private GameObject _observedObject;

    // Use this for initialization
    private void Start()
    {
        nv = GetComponent<NetworkView>();

        Lzwp.AddAfterInitializedAction(Init);
    }

    private void Init()
    {
        Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnPress += PressedFire;
        Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnRelease += ReleasedFire;
        if (!Lzwp.sync.isMaster)
        {
            foreach (var rg in FindObjectsOfType<Rigidbody>())
            {
                Destroy(rg);
            }
        }
    }

    private void PressedFire()
    {
        _observedObject = GetObservedObject();
        var grabable = Grabable(_observedObject);
        var interactable = Interactable(_observedObject);
        if (interactable != null) interactable.Interact();
        if (grabable == null) return;
        grabable.Grab(FlyStickObject);

        PlayerState = PlayerStateEnum.Carrying;
        _grabbedObject = grabable;
    }

    private void ReleasedFire()
    {
        _grabbedObject.Release();
        PlayerState = PlayerStateEnum.NotOccupied;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (PlayerState == PlayerStateEnum.Carrying)
                ReleasedFire();
            else if (PlayerState == PlayerStateEnum.NotOccupied)
                PressedFire();
        }
    }

    private void Awake()
    {
        PlayerState = PlayerStateEnum.NotOccupied;
    }


    private GameObject GetObservedObject()
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
        return grabable != null && ((MonoBehaviour)grabable).enabled ? grabable : null;
    }

    private IInteractable Interactable(GameObject gameObject)
    {
        var interactable = gameObject.GetComponent<IInteractable>();
        return interactable != null && ((MonoBehaviour)interactable).enabled ? interactable : null;
    }

    private enum PlayerStateEnum
    {
        Carrying,
        NotOccupied
    }
}

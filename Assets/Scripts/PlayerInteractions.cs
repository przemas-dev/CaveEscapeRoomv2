using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public float CarryInitialDistance = 1.0f;
    public float CarryMaxDistance = 2.0f;
    public float CarryMinDistance = 0.5f;
    public float CarrySpeed = 15.0f;
    public float InteractionRange = 2.0f;
    [Range(0.0f, 1.0f)] public float ScrollScale = 0.1f;


    private float _carryDistance;
    private GameObject _prevObservedObject;
    private GameObject _carriedObject;
    private IInteractable _interactedObject;
    private UIManager _uiManager;
    private UICamera _uiCamera;
    private Camera _camera;

    private PlayerState PlayerState { get; set; }

    private void Awake()
    {
        PlayerState = PlayerState.NotOccupied;
    }

    private void Start()
    {
        _uiManager = GameObject.FindObjectOfType<UIManager>();
        _uiCamera = GameObject.FindObjectOfType<UICamera>();
        _camera = Camera.main;
    }

    private void Update()
    {
        switch (PlayerState)
        {
            case PlayerState.Carrying:
                CalcCarryDistance();
                Carry(_carriedObject);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DropObject();
                }

                break;

            case PlayerState.Interacting:
                if (_interactedObject.Interact()) PlayerState = PlayerState.NotOccupied;
                break;

            case PlayerState.NotOccupied:
                GameObject observedObject = GetObservedObject();

                _uiManager.SetUpCrosshair(observedObject);
                if (observedObject == null) break;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(observedObject.CompareTag("actionObject")) 
                        _uiCamera.AttachCameraToObject(observedObject);

                    MakeInteraction(observedObject);
                }
                
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private GameObject GetObservedObject()
    {
        RaycastHit hit;
        Vector3 centralPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0.1f));
        if (Physics.Raycast(centralPoint, _camera.transform.forward, out hit))
        {
            if (hit.distance < InteractionRange)
                return hit.collider.gameObject;
        }

        return null;
    }

    private bool IsPickable(GameObject gameObject)
    {
        return gameObject.GetComponent<IPickable>() != null;
    }

    private void PickUp(GameObject observedObject)
    {
        PlayerState = PlayerState.Carrying;
        _carriedObject = observedObject;
        observedObject.GetComponent<Rigidbody>().useGravity = false;
        observedObject.GetComponent<Rigidbody>().angularDrag = 10.0f;
        _carryDistance = CarryInitialDistance;
        _uiManager.SetCrossHairColor(Color.green);
    }

    private void MakeInteraction(GameObject observedObject)
    {
        if (IsPickable(observedObject))
        {
            PickUp(observedObject);
            return;
        }

        var interactable = observedObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            if (interactable.Interact() == false)
            {
                _interactedObject = interactable;
                PlayerState = PlayerState.Interacting;
            }
        }
    }

    void DropObject()
    {
        PlayerState = PlayerState.NotOccupied;
        var rigidbody = _carriedObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.angularDrag = 0.05f;
        _uiManager.SetCrossHairColor(Color.white);
        _carriedObject = null;
    }

    void Carry(GameObject o)
    {
        o.GetComponent<Rigidbody>().velocity =
            (_camera.transform.position + _camera.transform.forward * _carryDistance - o.transform.position) *
            CarrySpeed;
    }

    void CalcCarryDistance()
    {
        _carryDistance += Input.mouseScrollDelta.y * ScrollScale;
        if (_carryDistance > CarryMaxDistance) _carryDistance = CarryMaxDistance;
        else if (_carryDistance < CarryMinDistance) _carryDistance = CarryMinDistance;
    }
}

public enum PlayerState
{
    Carrying,
    Interacting,
    NotOccupied,
}
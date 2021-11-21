using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour, IGrabable
{
    private bool _isCarried=false;
    private Rigidbody _rigidbody;
    private GameObject _grabber;
    private float _carryDistance = 1.0f;
    
    public float CarrySpeed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        IsGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrabbed) Carry();
    }

    public bool IsGrabbed { get; set; }
    public void Grab(GameObject grabber)
    {
        IsGrabbed = true;
        _grabber = grabber;
        _rigidbody.useGravity = false;
        _rigidbody.angularDrag = 10.0f;
    }

    private void Carry()
    {
        _rigidbody.velocity =
            (_grabber.transform.position + _grabber.transform.forward * _carryDistance - transform.position) *
            CarrySpeed;
    }
    
    public void Release()
    {
        IsGrabbed = false;
        _rigidbody.useGravity = true;
        _rigidbody.angularDrag = 0.05f;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour, IGrabable
{
    private bool _isCarried=false;
    private Rigidbody _rigidbody;
    private GameObject _grabber;
    private float _carryDistance;
    private Quaternion _rotationFactor;
    
    public float CarrySpeed = 15.0f;
    // Start is called before the first frame update
    protected void Start()
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

    public virtual void Grab(GameObject grabber)
    {
        IsGrabbed = true;
        _grabber = grabber;
        _rigidbody.useGravity = false;
        _rigidbody.angularDrag = 10.0f;
        _carryDistance = (transform.position - grabber.transform.position).magnitude;
        _rotationFactor = Quaternion.Inverse(_grabber.transform.rotation) * transform.rotation;
    }

    private void Carry()
    {
        _rigidbody.velocity =
            (_grabber.transform.position + _grabber.transform.forward * _carryDistance - transform.position) *
            CarrySpeed;
        transform.rotation = _grabber.transform.rotation * _rotationFactor;
    }
    
    public virtual void Release()
    {
        IsGrabbed = false;
        _rigidbody.useGravity = true;
        _rigidbody.angularDrag = 0.05f;
    }
}

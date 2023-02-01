using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Motion : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _previousVelocity;
    private Vector3 _position;
    private Vector3 _previousPosition;
    private Vector3 _acceleration;

    private MaterialPropertyBlock properties;
    // Start is called before the first frame update
    void Start()
    {
        _velocity = Vector3.zero;
        _acceleration = Vector3.zero;
        _position = transform.position;
        properties = new MaterialPropertyBlock();
    }

    private void FixedUpdate()
    {
        _acceleration = (_velocity - _previousVelocity) / Time.fixedDeltaTime;
        _velocity = (transform.position - _previousPosition) / Time.fixedDeltaTime;
        _previousVelocity = _velocity;
        _previousPosition = transform.position;
        properties.SetVector("_Acceleration", _acceleration);
        properties.SetVector("_Velocity", _velocity);
        GetComponent<Renderer>().SetPropertyBlock(properties);
        //scale in the direction of the velocity
        //transform.localScale = new Vector3(1, 1, _velocity.magnitude);
        Debug.Log(_velocity);
        Debug.Log(_velocity.magnitude);
    }

    // void FixedUpdate()
    // {
    //     _acceleration = ((GetComponent<Rigidbody>().velocity - _velocity) / Time.fixedDeltaTime);
    //     _velocity = GetComponent<Rigidbody>().velocity;
    //     
    //     properties.SetVector("_Acceleration", _acceleration);
    //     properties.SetVector("_Velocity", _velocity);
    //     GetComponent<Renderer>().SetPropertyBlock(properties);
    //     Debug.Log(_acceleration);
    // }
}

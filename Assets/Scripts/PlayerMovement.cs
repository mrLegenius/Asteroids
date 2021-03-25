using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float accelerationRate;
    [SerializeField] private float decelerationRate;

    [SerializeField] private float rotationRate;
    
    
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private bool _isAccelerating;
    private int _rotationDirection;
    private void Awake()
    {
        _transform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _isAccelerating = Input.GetKey(KeyCode.W);

        _rotationDirection = Input.GetKey(KeyCode.A) ? 1 : Input.GetKey(KeyCode.D) ? -1 : 0;
    }

    private void FixedUpdate()
    {
        if (_isAccelerating)
            _rigidbody2D.AddForce(accelerationRate * (Vector2) _transform.right, ForceMode2D.Force);

        _rigidbody2D.rotation += _rotationDirection * rotationRate * Time.fixedDeltaTime;
    }
}

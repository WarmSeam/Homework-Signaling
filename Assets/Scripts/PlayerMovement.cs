using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _moveSpeed = 0.3f;
    [SerializeField] private float _rotateSpeed = 6f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;

    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float vertical = Input.GetAxis(Vertical);

        _rigidbody.MovePosition(_rigidbody.position + transform.forward * vertical * _moveSpeed);
    }

    private void Rotate()
    {
        float rotation = Input.GetAxis(Horizontal);

       Quaternion deltaRotation = Quaternion.Euler(0, rotation * _rotateSpeed, 0);

        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }
}


using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private float _rotateSpeed = 200f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;

    }

    private void OnEnable()
    {
        _input.InputChanged += HandleInput;
    }

    private void OnDisable()
    {
        _input.InputChanged += HandleInput;
    }

    private void HandleInput(float rotation, float direction)
    {
        Rotate(rotation);
        Move(direction);
    }

    private void Rotate(float rotation)
    {
        Quaternion deltaRotation = Quaternion.Euler(0, rotation * _rotateSpeed * Time.deltaTime, 0);

        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }

    private void Move(float direction)
    {
        _rigidbody.MovePosition(_rigidbody.position + transform.forward * direction * _moveSpeed * Time.deltaTime);
    }

}


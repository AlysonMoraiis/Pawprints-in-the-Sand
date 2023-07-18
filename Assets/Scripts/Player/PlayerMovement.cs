using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Vector2 _movement;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetMovement(InputAction.CallbackContext value)
    {
        _movement = value.ReadValue<Vector2>().normalized;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_movement.x * _speed, _movement.y * _speed);
    }
}

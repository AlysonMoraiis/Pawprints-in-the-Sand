using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _dustParticlePrefab;
    [SerializeField] private Transform _dustLocation;


    private float _dustInstantiateTime = 0.6f;
    private float _dustLastTime;
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
        if (_movement.magnitude > 0)
        {
            Instantiate(_dustParticlePrefab, _dustLocation.position, _dustLocation.rotation);
            _dustLastTime = Time.time;
        }

        _rigidbody.velocity = new Vector2(_movement.x * _speed, _movement.y * _speed);
    }
}

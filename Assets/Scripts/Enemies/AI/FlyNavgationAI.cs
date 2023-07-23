using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyNavgationAI : MonoBehaviour
{
    [SerializeField] private float _detectDistance;
    [SerializeField] private float _stopChaseDistance;

    private GameObject _player;
    private NavMeshAgent _agent;

    private bool _canChase = false;

    void Start()
    {
        SetupAgent();
    }

    private void SetupAgent()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckCanChase();
    }

    private void CheckCanChase()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        _canChase = (_detectDistance > distance);

        if (_canChase)
        {
            _agent.SetDestination(_player.transform.position);

            if (_stopChaseDistance > distance)
            {
                _canChase = false;
            }
        }
    }
}

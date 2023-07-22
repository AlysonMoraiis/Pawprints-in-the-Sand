using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyNavgationAI : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }
}

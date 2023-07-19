using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletPosition;
    [SerializeField] private float _distanceToShoot;
    [SerializeField] private float _shootCooldown;

    private float _fireCooldown;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        _fireCooldown += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if(_fireCooldown > _shootCooldown && distance < _distanceToShoot)
        {
            _fireCooldown = 0;
            Shoot();
        }    
    }

    private void Shoot()
    {
        Instantiate(_bullet, _bulletPosition.position, Quaternion.identity);
    }
}

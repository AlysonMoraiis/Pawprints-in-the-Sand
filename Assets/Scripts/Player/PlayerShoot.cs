using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private PlayerData _playerData;

    private bool _isShooting;
    private Vector2 _shootDirection;
    private float _lastFire;

    public void SetShoot(InputAction.CallbackContext value)
    {
        _isShooting = true;

        value = SetBulletDirection(value);

        if (value.canceled)
        {
            _isShooting = false;
            _shootDirection = Vector2.zero;
        }
    }

    private InputAction.CallbackContext SetBulletDirection(InputAction.CallbackContext value)
    {
        Vector2 shootDirection = value.ReadValue<Vector2>();

        if (shootDirection.x != 0 && shootDirection.y == 0)
        {
            _shootDirection = value.ReadValue<Vector2>();
        }

        else if (shootDirection.y != 0 && shootDirection.x == 0)
        {
            _shootDirection = value.ReadValue<Vector2>();
        }

        return value;
    }

    private void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation) as GameObject;
        SetShootRotation(x, y);
        bullet.transform.rotation = SetShootRotation(x, y);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x < 0) ? Mathf.Floor(x) * _playerData.BulletSpeed : Mathf.Ceil(x) * _playerData.BulletSpeed + Random.Range(1f, -1f),
            (y < 0) ? Mathf.Floor(y) * _playerData.BulletSpeed : Mathf.Ceil(y) * _playerData.BulletSpeed + Random.Range(1f, -1f)
        );

    }

    private Quaternion SetShootRotation(float x, float y)
    {
        if (x > 0)
        {
            return Quaternion.Euler(0f, 0f, -90f);
        }
        else if (x < 0)
        {
            return Quaternion.Euler(0f, 0f, 90f);
        }
        else if (y > 0)
        {
            return Quaternion.Euler(0f, 0f, 0);
        }
        else
        {
            return Quaternion.Euler(0f, 0f, 180f);
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > _lastFire + _playerData.FireCooldown && _isShooting)
        {
            Shoot(_shootDirection.x, _shootDirection.y);
            _lastFire = Time.time;
        }
    }
}

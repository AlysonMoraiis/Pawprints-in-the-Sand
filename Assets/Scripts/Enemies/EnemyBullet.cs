using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private ParticleSystem _destroyParticlePrefab;

    private GameObject _player;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = _player.transform.position - transform.position;
        _rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * _speed;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);

        StartCoroutine(DestroyGameObject(_timeToDestroy, 0.1f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(DestroyGameObject(0, 0));
        }
    }

    private IEnumerator DestroyGameObject(float i, float o)
    {
        yield return new WaitForSeconds(i);
        StartCoroutine(SpriteFadeOut(o));
        yield return new WaitForSeconds(o);
        Instantiate(_destroyParticlePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator SpriteFadeOut(float fadeOutTime)
    {
        float startTime = Time.time;
        Color startColor = _spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (Time.time < startTime + fadeOutTime)
        {
            float t = (Time.time - startTime) / fadeOutTime;
            _spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        _spriteRenderer.color = endColor;
    }
}

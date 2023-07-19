using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private ParticleSystem _destroyParticlePrefab;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(DestroyGameObject(_timeToDestroy, 0.15f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(DestroyGameObject(0, 0));
    }

    private IEnumerator DestroyGameObject(float i, float o)
    {
        yield return new WaitForSeconds(i);
        _rigidbody.gravityScale = 4;
        yield return new WaitForSeconds(o);
        Instantiate(_destroyParticlePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

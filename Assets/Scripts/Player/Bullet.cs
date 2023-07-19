using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private ParticleSystem _destroyParticlePrefab;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(DestroyGameObject(_timeToDestroy, 0.1f));
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
        StartCoroutine(SpriteFadeOut(o));
        yield return new WaitForSeconds(0.05f);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Material _flashMaterial;
    private Material _defaultMaterial;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _health -= 1;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        int flashs = 3;
        float flashDuration = 0.05f;

        for (int i = 0; i < flashs; i++)
        {
            _spriteRenderer.material = _flashMaterial;
            yield return new WaitForSeconds(flashDuration);
            _spriteRenderer.material = _defaultMaterial;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}

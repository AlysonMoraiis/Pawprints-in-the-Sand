using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}

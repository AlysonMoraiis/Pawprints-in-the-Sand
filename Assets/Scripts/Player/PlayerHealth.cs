using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private SpriteRenderer _spriteRenderer;

    private int _health;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _health = _maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }

        if (other.gameObject.CompareTag("CollectableHealth"))
        {
            if (_health <= _maxHealth)
            {
                _health += 1;
            }
        }
    }

    private void TakeDamage()
    {
        _health -= 1;

        if (_health <= 0)
        {
            Debug.Log("Death");
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

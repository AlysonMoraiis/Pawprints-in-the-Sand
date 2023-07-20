using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private SpriteRenderer _spriteRenderer;

    private bool _canDamage = true;

    [HideInInspector] public int _health;
    public event Action OnHealthChange;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health = _playerData.PlayerMaxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }

        if (other.gameObject.CompareTag("CollectableHealth"))
        {
            Healing();
        }
    }

    private void TakeDamage()
    {
        if (_canDamage)
        {
            _health -= 1;
            OnHealthChange?.Invoke();

            if (_health <= 0)
            {
                _health = 0;
                Debug.Log("Death");
            }

            StartCoroutine(DamageFlash());
        }
    }

    private IEnumerator DamageFlash()
    {
        int flashs = 3;
        float flashDuration = 0.05f;
        _canDamage = false;

        for (int i = 0; i < flashs; i++)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }

        _canDamage = true;
    }

    private void Healing()
    {
        _health += 1;
        if (_health > _playerData.PlayerMaxHealth)
        {
            _health = _playerData.PlayerMaxHealth;
        }

        OnHealthChange?.Invoke();
    }

}

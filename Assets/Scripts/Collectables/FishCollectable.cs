using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishCollectable : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Vector3 _originalScale;
    private Vector3 _scaleTo;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.2f;

        transform.DOScale(_scaleTo, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DestroyGameObject());
        }
    }

    private IEnumerator DestroyGameObject()
    {
        StartCoroutine(SpriteFadeOut(0.1f));
        yield return new WaitForSeconds(0.5f);
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

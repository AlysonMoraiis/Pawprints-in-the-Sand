using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;

    void Start()
    {
        Invoke("DestroyGameObject", _timeToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

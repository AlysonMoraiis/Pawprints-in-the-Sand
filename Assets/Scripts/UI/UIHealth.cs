using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    private void Start()
    {
        CheckAmountOfHearts();
    }
    
    private void OnEnable()
    {
        _playerHealth.OnHealthChange += HealthCheck;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChange -= HealthCheck;
    }

    private void HealthCheck()
    {
        if (_playerHealth._health > _hearts.Length)
        {
            _playerHealth._health = _hearts.Length;
        }
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < _playerHealth._health)
            {
                _hearts[i].sprite = _fullHeart;
                //_hearts[i].GetComponent<Animator>().Rebind();
            }
            else
            {
                _hearts[i].sprite = _emptyHeart;
                //_hearts[i].GetComponent<Animator>().Play("HeartBreak");
            }
        }
    }


    private void CheckAmountOfHearts()
    {
        for (int i = 0; i < _playerData.PlayerMaxHealth; i++)
        {
            _hearts[i].enabled = true;
        }
    }
}

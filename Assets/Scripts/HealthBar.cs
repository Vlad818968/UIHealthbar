using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Player _player;

    private float _health;
    private float _recoveryRate = 25f;
    private Coroutine _healthCoroutine;

    private void Start()
    {
        SetMaxHealth();
    }

    private void SetMaxHealth()
    {
        _healthSlider.maxValue = _player.CurrentHealth;
        _healthSlider.value = _healthSlider.maxValue;
        _health = _player.CurrentHealth;
        _healthText.text = _healthSlider.value.ToString();
    }

    private void OnEnable()
    {
        _player.HealthChanged += StartHealthUpdate;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= StartHealthUpdate;
    }

    private void StartHealthUpdate(int currentHealth)
    {
        if (_healthCoroutine != null)
        {
            StopCoroutine(_healthCoroutine);
        }

        _healthCoroutine = StartCoroutine(HealthUpdate(currentHealth));
    }

    private IEnumerator HealthUpdate(int currentHealth)
    {
        while (_health != currentHealth)
        {
            _health = Mathf.MoveTowards(_health, currentHealth, _recoveryRate * Time.deltaTime);
            int health = (int)_health;
            _healthText.text = health.ToString();
            _healthSlider.value = health;
            yield return null;
        }
    }
}

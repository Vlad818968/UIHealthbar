using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private Slider _healthSlider;

    private float _health;
    private float _recoveryRate = 25f;
    private Coroutine _healthCoroutine;
    
    private void Start()
    {
        _healthText.text = _healthSlider.value.ToString();
    }

    public void SetMaxHealth(Player player)
    {
        _healthSlider.maxValue = player.MaxHealth;
        _healthSlider.value = _healthSlider.maxValue;
        _health = player.MaxHealth;
    }

    public void StartHealthUpdate(Player player)
    {
        if (_healthCoroutine != null)
        {
            StopCoroutine(_healthCoroutine);
        }

        _healthCoroutine = StartCoroutine(HealthUpdate(player));
    }

    private IEnumerator HealthUpdate(Player player)
    {
        while (_health != player.CurrentHealth)
        {
           _health = Mathf.MoveTowards(_health, player.CurrentHealth, _recoveryRate * Time.deltaTime);
            int health = (int)_health;
           _healthText.text = health.ToString();
            _healthSlider.value = health;
            yield return null;
        }
    }

}

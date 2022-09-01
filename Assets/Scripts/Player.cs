using UnityEngine;
using UnityEngine.Events;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    public int CurrentHealth => _currentHealth;

    public Action<int> HealthChanged;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        HealthChanged?.Invoke(_currentHealth);
    }

    public void RestoreHealth(int treatment)
    {
        _currentHealth += treatment;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        HealthChanged?.Invoke(_currentHealth);
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthBar _healthBar;

    private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.StartHealthUpdate(this);
    }

    public void RestoreHealth(int treatment)
    {
        _currentHealth += treatment;
        _healthBar.StartHealthUpdate(this);
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(this);
    }

    private void Update()
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
}

using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public event Action <int> OnHealthChanged;
    public event Action OnDead;

    [SerializeField] private int _maxHealth;

    private int _health;

    private void Start()
    {
        SetHealthOnMaxValue();
    }

    public void SetHealthOnMaxValue()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int value)
    {
        if (_health == 0)
            return;

        if (value <= 0) 
            return;

        _health = Math.Clamp(_health - value, 0, _maxHealth);
        OnHealthChanged?.Invoke(_health);

        if (_health <= 0)
            OnDead?.Invoke();
    }
}

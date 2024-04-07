using System;
using UnityEngine;

public class StabilizerTerminal : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject normalTerminalVisuals;
    [SerializeField] private GameObject brokenTerminalVisuals;

    [SerializeField] private int health;
    private int _currentHealth;

    [SerializeField] private InteractableHint interactableHint;

    public bool IsBroken {get; private set;}

    public event Action Broked;

    private void Awake()
    {
        _currentHealth = health;
        interactableHint.On();

        if (_currentHealth <= 0 )
        {
            IsBroken = true;
            interactableHint.Off();
            Broked?.Invoke();
        }
    }

    public void TakeDamage(int value)
    {
        if (IsBroken) return;

        _currentHealth -= value;

        if (_currentHealth < 0 ) 
        {
            IsBroken = true;
            Broked?.Invoke();
            interactableHint.Off();

            normalTerminalVisuals.SetActive(false);
            brokenTerminalVisuals.SetActive(true);
        }
    }
}

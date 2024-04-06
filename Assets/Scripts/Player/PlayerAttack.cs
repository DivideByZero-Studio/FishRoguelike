using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Colliders")]
    [SerializeField] private PlayerAttackCollider _primaryAttackCollider;
    [SerializeField] private PlayerAttackCollider _secondaryAttackCollider;

    [Header("Specs")]
    [SerializeField] private int _primaryAttackDamage;
    [SerializeField] private int _secondaryAttackDamage;
    [SerializeField] private float _attackCooldown;

    private Vector2 _lastMoveDirection;

    private PlayerController _characterController;
    private PlayerMovement _playerMovement;
    private Timer _timer;

    public event Action OnPrimaryAttack;
    public event Action OnSecondaryAttack;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _characterController = GetComponent<PlayerController>();

        _timer = new Timer(_attackCooldown);

        _lastMoveDirection = Vector2.zero;
    }

    private void Update()
    {
        _timer.DecreaseTime();
    }

    private void OnEnable()
    {
        _characterController.OnPrimaryAttack += PrimaryAttack;
        _primaryAttackCollider.DamageableEntered += PrimaryAttackGiveDamage;

        _characterController.OnSecondaryAttack += SecondaryAttack;
        _secondaryAttackCollider.DamageableEntered += SecondaryAttackGiveDamage;
    }
    private void OnDisable()
    {
        _characterController.OnPrimaryAttack -= PrimaryAttack;
        _primaryAttackCollider.DamageableEntered -= PrimaryAttackGiveDamage;

        _characterController.OnSecondaryAttack -= SecondaryAttack;
        _secondaryAttackCollider.DamageableEntered -= SecondaryAttackGiveDamage;
    }

    private void PrimaryAttack()
    {
        if (_timer.IsReady == false)
            return;

        _timer.Reset();
        OnPrimaryAttack?.Invoke();
        StartCoroutine(AttackRoutine(_primaryAttackCollider));
    }

    private void SecondaryAttack()
    {
        if (_timer.IsReady == false)
            return;

        _timer.Reset();
        OnSecondaryAttack?.Invoke();
        StartCoroutine(AttackRoutine(_secondaryAttackCollider));
    }

    private void PrimaryAttackGiveDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_primaryAttackDamage);
    }

    private void SecondaryAttackGiveDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_secondaryAttackDamage);
    }

    private IEnumerator AttackRoutine(PlayerAttackCollider attackCollider)
    {
        SetColliderRotation(attackCollider);

        attackCollider.Enable();
        yield return new WaitForSeconds(0.5f);
        attackCollider.Disable();
    }

    private void SetColliderRotation(PlayerAttackCollider attackCollider)
    {
        attackCollider.SetRotation(_playerMovement.LastMoveDirection);
    }
}

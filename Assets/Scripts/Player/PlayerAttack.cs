using System.Collections;
using UnityEngine;

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
    private Timer _timer;

    private void Awake()
    {
        _characterController = GetComponent<PlayerController>();

        _timer = new Timer(_attackCooldown);

        _lastMoveDirection = Vector2.zero;
    }

    private void Update()
    {
        var moveDirection = _characterController.GetMovementNormalizedVector();
        if (moveDirection != Vector2.zero)
            _lastMoveDirection = moveDirection;

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

        StartCoroutine(AttackRoutine(_primaryAttackCollider));
    }

    private void SecondaryAttack()
    {
        if (_timer.IsReady == false)
            return;

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

        _timer.Reset();
    }

    private void SetColliderRotation(PlayerAttackCollider attackCollider)
    {
        attackCollider.SetRotation(_lastMoveDirection);
    }
}

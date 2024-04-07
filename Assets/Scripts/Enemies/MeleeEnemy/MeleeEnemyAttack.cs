using System;
using System.Collections;
using UnityEngine;

public class MeleeEnemyAttack : EnemyAttack
{
    public event Action OnAttacked;
    public event Action OnLeft;

    public static readonly float AttackRange = 1.5f;

    [SerializeField] private AttackCollider _attackCollider;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _attackAnimationDuration;

    private Timer _attackTimer;
    private Transform _transform;
    private Transform _playerTransform;

    private const float _colliderLiveTime = 0.1f;

    private void Awake()
    {
        _attackTimer = new Timer(_cooldown + _colliderLiveTime + _attackAnimationDuration);
        _transform = transform;
    }

    private void Update()
    {
        _attackTimer.DecreaseTime();
    }

    public override void StartAttack(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public override void Attack()
    {
        if (_playerTransform == null)
        {
            return;
        }

        if (_attackTimer.IsReady)
        {
            Debug.Log("Attack");
            StartCoroutine(AttackRoutine());
            _attackTimer.Reset();
        }

        if ((_transform.position - _playerTransform.position).magnitude > AttackRange)
        {
            OnLeft?.Invoke();
        } 
    }

    private IEnumerator AttackRoutine()
    {
        SetColliderRotation();
        _attackCollider.Enable();
        yield return new WaitForSeconds(_colliderLiveTime);
        _attackCollider.Disable();
        yield return new WaitForSeconds(_attackAnimationDuration);
        OnAttacked?.Invoke();
    }

    private void SetColliderRotation()
    {
        Vector3 direction = _playerTransform.position - _transform.position;
        _attackCollider.SetRotation(direction);
    }

    private void AttackGiveDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
    }

    private void OnEnable()
    {
        _attackCollider.DamageableEntered += AttackGiveDamage;
    }

    private void OnDisable()
    {
        _attackCollider.DamageableEntered -= AttackGiveDamage;
    }
}

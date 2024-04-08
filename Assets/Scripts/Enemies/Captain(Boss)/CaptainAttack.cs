using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainAttack : EnemyAttack
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _preparingTime;
    [SerializeField, Range(0, 1)] private float _firstAttackChance;
    [SerializeField] ShotPoint _shotPointFirstAttack;
    [SerializeField] ShotPoint _shotPointSecondaryAttack;

    private Timer _timer;
    private System.Random _random;
    private Transform _playerTransform;
    private float _colliderLifeTime = 0.2f;
    private ShotPoint _currentShotPoint;
    private Vector3 _direction;

    private delegate void AttackDelegate();

    private AttackDelegate AttackByType;

    private void Awake()
    {
        _timer = new Timer(_cooldown + _preparingTime + _colliderLifeTime);
        _random = new System.Random();
        _shotPointFirstAttack.gameObject.SetActive(false);
        _shotPointSecondaryAttack.gameObject.SetActive(false);
    }

    private void Update()
    {
        _timer.DecreaseTime();
    }
    public override void StartAttack(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public override void Attack()
    {
        if (_timer.IsReady == false)
            return;

        StartCoroutine(AttackRoutine());
        _timer.Reset();
    }

    public override void StopAttack()
    {
        _shotPointFirstAttack.gameObject.SetActive(false);
        _shotPointSecondaryAttack.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    private IEnumerator AttackRoutine()
    {
        ChooseAttackType();
        _currentShotPoint.gameObject.SetActive(true);
        _currentShotPoint.transform.right = _playerTransform.position - _currentShotPoint.transform.position;
        yield return new WaitForSeconds(_preparingTime);
        AttackByType();
        _currentShotPoint.StartActiveRoutine(_colliderLifeTime);
        yield return new WaitForSeconds(_colliderLifeTime);
        _currentShotPoint.gameObject.SetActive(false);
        InvokeOnAttack();
    }

    private void ChooseAttackType()
    {
        if (_random.NextDouble() <= _firstAttackChance)
        {
            AttackByType = FirstAttack;
            _currentShotPoint = _shotPointFirstAttack;
            return;
        }
        AttackByType = SecondaryAttack;
        _currentShotPoint = _shotPointSecondaryAttack;
    }

    private void AttackGiveDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
    }

    private void OnEnable()
    {
        _shotPointFirstAttack.DamagableEntered += AttackGiveDamage;
        _shotPointSecondaryAttack.DamagableEntered += AttackGiveDamage;
    }

    private void OnDisable()
    {
        _shotPointFirstAttack.DamagableEntered -= AttackGiveDamage;
        _shotPointSecondaryAttack.DamagableEntered -= AttackGiveDamage;
    }

    private void FirstAttack()
    {

    }

    private void SecondaryAttack()
    {

    }
    
}

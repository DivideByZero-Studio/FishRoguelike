using System.Collections;
using UnityEngine;

public class SlideEnemyAttack : EnemyAttack
{
    [SerializeField] private AttackCollider _attackCollider;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;

    private Transform _playerTransform;
    private Transform _transform;
    private Vector3 _direction;
    private Rigidbody2D _rigidbody2D;

    private Timer _timer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = transform;
        _timer = new Timer(_cooldown + _duration);
    }

    private void Update()
    {
        _timer.DecreaseTime();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_transform.position + (_direction * _speed * Time.fixedDeltaTime));
    }

    public override void Attack()
    {
        if (_timer.IsReady)
        {
            StartCoroutine(AttackRoutine());
            _timer.Reset();
        }

        if ((_transform.position - _playerTransform.position).magnitude > _attackRange)
        {
            InvokeOnLeft();
        }
    }

    public override void StartAttack(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void SetDirection(Vector3 playerPosition)
    {
        _direction = (playerPosition - _transform.position).normalized;
    }

    private IEnumerator AttackRoutine()
    {
        _attackCollider.Enable();
        SetDirection(_playerTransform.position);
        yield return new WaitForSeconds(_duration);
        _attackCollider.Disable();
        _direction = Vector3.zero;
        InvokeOnAttack();
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

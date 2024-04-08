using System;
using UnityEngine;

public class MeleeEnemyApproach : MonoBehaviour
{
    public event Action OnApproached;

    [SerializeField] private float _speed;

    private Transform _playerTransform;
    private Transform _transform;
    private EnemyAttack _attack;
    private float _attackRange;

    private void Awake()
    {
        _attack = GetComponent<EnemyAttack>();
        _attackRange = _attack.GetAttackRange();
    }

    public void StartApproach(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _transform = transform;
    }

    public void Approach()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _playerTransform.position, _speed * Time.deltaTime);
        if (Vector3.Distance(_transform.position, _playerTransform.position) <= _attackRange)
        {
            OnApproached?.Invoke();
        }
    }
}

using UnityEngine;

public abstract class EnemyStateMachine : StateMachine
{
    protected Enemy _enemy;
    protected Transform _playerTransform;

    protected virtual void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _playerTransform = _enemy.GetPlayerTransform();
    }
}

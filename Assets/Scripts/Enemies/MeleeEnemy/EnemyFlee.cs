using System;
using System.Collections;
using UnityEngine;

public class EnemyFlee : MonoBehaviour
{
    public event Action OnEnded;

    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
    [SerializeField] private EnemyAttackRange _attackRange;

    private Transform _transform;
    private Transform _playerTransform;
    private Vector3 _targetFleePosition;
    private System.Random _random;

    private void Awake()
    {
        _random = new System.Random();
        _transform = transform;
    }

    public void StartFlee(Transform playerTransform)
    {
        _attackRange.Disable();
        _playerTransform = playerTransform;
        _targetFleePosition = ChooseRandomVectorDirection();
        StartCoroutine(FleeRoutine());
    }

    public void Flee()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _targetFleePosition, _speed * Time.deltaTime);
    }

    public void StopFlee()
    {
        StopAllCoroutines();
        _attackRange.Enable();
    }

    private IEnumerator FleeRoutine()
    {
        yield return new WaitForSeconds(_duration);
        OnEnded?.Invoke();
    }

    private Vector3 GetRandomVector()
    {
        return new Vector3((float)_random.NextDouble(), (float)_random.NextDouble());
    }

    private Vector3 ChooseRandomVectorDirection()
    {
        Vector3 vector = GetRandomVector();
        if (transform.position.x >= _playerTransform.position.x)
        {
            vector = new Vector3(vector.x * 1f, vector.y);
        }
        else
        {
            vector = new Vector3(vector.x * -1f, vector.y);
        }

        if (transform.position.y >= _playerTransform.position.y)
        {
            vector = new Vector3(vector.x, vector.y * 1f);
        }
        else
        {
            vector = new Vector3(vector.x, vector.y * -1);
        }

        return vector;
    }
}

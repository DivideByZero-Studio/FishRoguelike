using System;
using System.Collections;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public event Action OnEnded;

    [SerializeField] private float _speed;
    [SerializeField] private float _minDistance = 0.5f;
    [SerializeField] private float _maxDistance = 2f;
    [SerializeField] private LayerMask _obstaclesLayer;

    private const float _rayLength = 1.5f;
    private const float _maxDuration = 5f;

    private Vector3 _targetPosition;
    private Transform _transform;
    private System.Random _random;

    private void Awake()
    {
        _transform = transform;
        _random = new System.Random();
    }

    public void StartWalk()
    {
        SetTargetPosition();
        StartCoroutine(DurationRoutine());
    }

    public void Walk()
    {
        Ray2D ray = new Ray2D(_transform.position, _targetPosition);

        if (Physics2D.Raycast(ray.origin, ray.direction, _rayLength, _obstaclesLayer) || _transform.position == _targetPosition)
        {
            OnEnded?.Invoke();
        }

        _transform.position = Vector3.MoveTowards(_transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void StopWalk()
    {
        _targetPosition = _transform.position;
        StopAllCoroutines();
    }

    private void SetTargetPosition()
    {
        Vector2 direction = new Vector2((float)_random.NextDouble() * GetRandomSign(), (float)_random.NextDouble() * GetRandomSign()).normalized;
        float distance = (float)_random.NextDouble() * (_maxDistance - _minDistance) + _minDistance;
        _targetPosition = new Vector3(_transform.position.x + direction.x * distance, _transform.position.y + direction.y * distance);
    }

    private int GetRandomSign()
    {
        double value = _random.NextDouble();

        if (value >= 0.5f)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private IEnumerator DurationRoutine()
    {
        yield return new WaitForSeconds(_maxDuration);
        OnEnded?.Invoke();
    }
}

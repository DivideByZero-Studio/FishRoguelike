using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class EnemyIdle : MonoBehaviour
{
    public event Action OnEnded;

    [SerializeField] private float _minDuration = 1f;
    [SerializeField] private float _maxDuration = 3f;

    private float _randomDuration;

    private Random _random;
    private Coroutine _coroutine;

    private void Awake()
    {
        _random = new Random();
    }

    public void StartIdle()
    {
        _randomDuration = (float) _random.NextDouble() * (_maxDuration - _minDuration) + _minDuration;
        _coroutine = StartCoroutine(IdleRoutine());
    }

    public void StopIdle()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator IdleRoutine()
    {
        yield return new WaitForSeconds(_randomDuration);
        OnEnded?.Invoke();
    }
}

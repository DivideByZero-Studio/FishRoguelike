using System;
using System.Collections;
using UnityEngine;

public class EnemyFlee : MonoBehaviour
{
    public event Action OnEnded;

    [SerializeField] private float _speed;
    [Space, Header("StepAway")]
    [SerializeField] private float _duration;

    private Vector3 _targetFleePosition;

    public void StartFlee(Transform playerTransform)
    {
        // Math formula to calculate direction to step away
        StartCoroutine(FleeRoutine());
    }

    public void Flee()
    {

    }

    public void StopFlee()
    {
        StopAllCoroutines();
    }

    private IEnumerator FleeRoutine()
    {
        yield return new WaitForSeconds(_duration);
        OnEnded?.Invoke();
    }
}

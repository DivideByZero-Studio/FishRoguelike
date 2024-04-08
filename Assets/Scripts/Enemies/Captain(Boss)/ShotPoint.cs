using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPoint : MonoBehaviour
{
    public event Action<IDamageable> DamagableEntered;
    [SerializeField] Collider2D[] _colliders;

    private float _time;

    private void Awake()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }
    public void StartActiveRoutine(float time)
    {
        _time = time;
        StartCoroutine(ActiveRoutine());
    }

    private IEnumerator ActiveRoutine()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }

        yield return new WaitForSeconds(_time);

        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        foreach (var collider in _colliders)
        {
            collider.gameObject.GetComponent<AttackCollider>().DamageableEntered -= OnDamageableEntered;
            collider.enabled = false;
        }
    }


    private void OnEnable()
    {
        foreach (var collider in _colliders)
        {
            collider.gameObject.GetComponent<AttackCollider>().DamageableEntered += OnDamageableEntered;
        }
    }

    private void OnDamageableEntered(IDamageable damagable)
    {
        DamagableEntered?.Invoke(damagable);
    }
}

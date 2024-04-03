using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public event Action OnDashEnd;

    [SerializeField] private float _timeToDash;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        enabled = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Debug.Log("Dashing");
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(_timeToDash);
        OnDashEnd?.Invoke();
    }
    
    public void Dash()
    {
        StartCoroutine(DashCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

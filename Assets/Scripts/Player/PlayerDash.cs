using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float _dashDurability;
    [SerializeField] private float _dashDistance;
    private Vector2 _dashDirection;

    private CharacterController _characterController;
    private Rigidbody2D _rigidbody;
    private WaitForFixedUpdate _waitForFixedUpdate;

    public event Action OnDashEnd;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _waitForFixedUpdate = new WaitForFixedUpdate();
    }

    public void StartDash()
    {
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        _dashDirection = _characterController.GetMovementNormalizedVector();

        var time = _dashDurability;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            Move();
            yield return _waitForFixedUpdate;
        }

        OnDashEnd?.Invoke();
    }

    private void Move()
    {
        var step = _dashDirection * _dashDistance * Time.fixedDeltaTime / _dashDurability;
        var nextPosition = _rigidbody.position + step;
        _rigidbody.MovePosition(nextPosition);
    }

}

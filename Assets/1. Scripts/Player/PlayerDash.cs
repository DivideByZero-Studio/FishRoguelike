using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashDurability;
    [SerializeField] private float _dashDistance;

    private Vector2 _dashDirection;

    private Rigidbody2D _rigidbody;
    private WaitForFixedUpdate _waitForFixedUpdate;
    private Collider2D _collider;

    private Timer _timer;

    public event Action OnDashEnd;

    [Inject] private GameInput gameInput;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _waitForFixedUpdate = new WaitForFixedUpdate();

        _timer = new Timer(_dashCooldown);
    }

    private void Update()
    {
        _timer.DecreaseTime();
    }

    public void Dash()
    {
        if (_timer.IsReady == false)
        {
            OnDashEnd?.Invoke();
            return;
        }

        _timer.Reset();
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        _dashDirection = gameInput.GetMovementNormalizedVector();
        _collider.enabled = false;

        var time = _dashDurability;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            MoveWhenDash();
            yield return _waitForFixedUpdate;
        }

        _collider.enabled = true;
        OnDashEnd?.Invoke();
    }

    private void MoveWhenDash()
    {
        var step = _dashDistance * Time.fixedDeltaTime * _dashDirection / _dashDurability;
        var nextPosition = _rigidbody.position + step;
        _rigidbody.MovePosition(nextPosition);
    }

}

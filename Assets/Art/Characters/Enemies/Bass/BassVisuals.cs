using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BassVisuals : MonoBehaviour
{
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private Health enemyHealth;

    private const string horizontalMovement = nameof(horizontalMovement);
    private const string verticalMovement = nameof(verticalMovement);
    private const string StartSlide = nameof(StartSlide);
    private const string StopSlide = nameof(StopSlide);
    private const string Death = nameof(Death);

    private Animator _animator;

    private Vector3 _previousPosition;
    private Vector2 _lastMoveDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        enemyAttack.OnAttackStarted += StartAttackAnimation;
        enemyAttack.OnAttack += StopAttackAnimation;
        enemyHealth.OnDead += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        enemyAttack.OnAttackStarted -= StartAttackAnimation;
        enemyAttack.OnAttack -= StopAttackAnimation;
        enemyHealth.OnDead -= PlayDeathAnimation;
    }

    private void Update()
    {
        var moveDirection = transform.position - _previousPosition;
        _previousPosition = transform.position;

        if (moveDirection != Vector3.zero)
        {
            _lastMoveDirection = moveDirection;
        }

        _animator.SetFloat(horizontalMovement, _lastMoveDirection.x);
        _animator.SetFloat(verticalMovement, _lastMoveDirection.y);
    }

    private void StartAttackAnimation()
    {
        _animator.SetTrigger(StartSlide);
    }

    private void StopAttackAnimation()
    {
        _animator.SetTrigger(StopSlide);
    }

    private void PlayDeathAnimation()
    {
        _animator.SetTrigger(Death);
    }
}
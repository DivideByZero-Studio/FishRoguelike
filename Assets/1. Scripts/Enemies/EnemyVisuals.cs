using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyVisuals : MonoBehaviour
{
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private Health enemyHealth;

    private const string horizontalMovement = nameof(horizontalMovement);
    private const string verticalMovement = nameof(verticalMovement);
    private const string Jab = nameof(Jab);
    private const string Kick = nameof(Kick);
    private const string Death = nameof(Death);

    private Animator _animator;
    private System.Random _random;

    private Vector3 _previousPosition;
    private Vector2 _lastMoveDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _random = new System.Random();
    }

    private void OnEnable()
    {
        enemyAttack.OnAttack += PlayAttackAnimation;
        enemyHealth.OnDead += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        enemyAttack.OnAttack -= PlayAttackAnimation;
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

    private void PlayAttackAnimation()
    {
        int num = _random.Next(2);
        switch(num)
        {
            case 0:
                _animator.SetTrigger(Jab);
                break;
            case 1:
                _animator.SetTrigger(Kick);
                break;
        }
    }

    private void PlayDeathAnimation()
    {
        _animator.SetTrigger(Death);
    }
}

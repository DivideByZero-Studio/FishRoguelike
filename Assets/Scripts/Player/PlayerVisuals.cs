using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Health _playerHealth;

    private Animator _animator;

    private Vector3 _lastPosition;

    private const string movementHorizontal = nameof(movementHorizontal);
    private const string movementVertical = nameof(movementVertical);
    private const string Jab = nameof(Jab);
    private const string Kick = nameof(Kick);
    private const string Death = nameof(Death);
    private const string isMoving = nameof(isMoving);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetMovementDirection();
        SetWalkParameter();
    }

    private void OnEnable()
    {
        _playerAttack.OnPrimaryAttack += PlayJabAnimation;
        _playerAttack.OnSecondaryAttack += PlayKickAnimation;
        _playerHealth.OnDead += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        _playerAttack.OnPrimaryAttack -= PlayJabAnimation;
        _playerAttack.OnSecondaryAttack -= PlayKickAnimation;
        _playerHealth.OnDead -= PlayDeathAnimation;
    }

    private void SetWalkParameter()
    {
        var position = transform.position;
        if (_lastPosition == position)
        {
            _animator.SetBool(isMoving, false);
        }
        else
        {
            _animator.SetBool(isMoving, true);
        }
        _lastPosition = position;
    }

    private void SetMovementDirection()
    {
        var direction = _playerMovement.LastMoveDirection;

        _animator.SetFloat(movementHorizontal, direction.x);
        _animator.SetFloat(movementVertical, direction.y);
    }

    private void PlayJabAnimation()
    {
        _animator.SetTrigger(Jab);
    }

    private void PlayKickAnimation()
    {
        _animator.SetTrigger(Kick);
    }

    private void PlayDeathAnimation()
    {
        _animator.SetTrigger(Death);
    }
}

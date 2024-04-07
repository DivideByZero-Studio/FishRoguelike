using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerMovement _playerMovement;

    private Animator _animator;

    private const string movementHorizontal = nameof(movementHorizontal);
    private const string movementVertical = nameof(movementVertical);
    private const string Jab = nameof(Jab);
    private const string Kick = nameof(Kick);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetMovementDirection();
    }

    private void OnEnable()
    {
        _playerAttack.OnPrimaryAttack += PlayJabAnimation;
        _playerAttack.OnSecondaryAttack += PlayKickAnimation;
    }

    private void OnDisable()
    {
        _playerAttack.OnPrimaryAttack -= PlayJabAnimation;
        _playerAttack.OnSecondaryAttack -= PlayKickAnimation;
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
}

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Health _playerHealth;

    [Header("Sounds")]
    [SerializeField] private AudioClip jabSound;
    [SerializeField] private AudioClip kickSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject stepSound;

    private Animator _animator;

    private Vector3 _lastPosition;
    private Vector3 _rightLocalScale = new(1, 1, 1);
    private Vector3 _leftLocalScale = new(-1, 1, 1);

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
            stepSound.SetActive(false);
            _animator.SetBool(isMoving, false);
        }
        else
        {
            stepSound.SetActive(true);
            _animator.SetBool(isMoving, true);
        }
        _lastPosition = position;
    }

    private void SetMovementDirection()
    {  
        if (_playerMovement.LastHorizontalDirection >= 0)
        {
            transform.localScale = _rightLocalScale;
        }
        else
        {
            transform.localScale = _leftLocalScale;
        }
        
        /*_animator.SetFloat(movementHorizontal, direction.x);
        _animator.SetFloat(movementVertical, direction.y);*/
    }

    private void PlayJabAnimation()
    {
        AudioManager.Instance.PlaySFX(jabSound);
        _animator.SetTrigger(Jab);
    }

    private void PlayKickAnimation()
    {
        AudioManager.Instance.PlaySFX(kickSound);
        _animator.SetTrigger(Kick);
    }

    private void PlayDeathAnimation()
    {
        AudioManager.Instance.PlaySFX(deathSound);
        _animator.SetTrigger(Death);
    }
}

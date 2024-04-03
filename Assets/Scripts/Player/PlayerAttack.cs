using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private BoxCollider2D _attackTrigger;

    private CharacterController _characterController;

    private Timer _timer;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _timer = new Timer(_cooldown);
    }

    private void Update()
    {
        _timer.DecreaseTime();
    }

    private void PrimaryAttack()
    {
        if (_timer.IsReady == false)
            return;

        Debug.Log("Primary Attacked");
        _timer.Reset();
    }

    private void SecondaryAttack()
    {
        if (_timer.IsReady == false)
            return;

        Debug.Log("Secondary Attacked");
        _timer.Reset();
    }

    private void OnEnable()
    {
        _characterController.OnPrimaryAttack += PrimaryAttack;
        _characterController.OnSecondaryAttack += SecondaryAttack;
    }
    private void OnDisable()
    {
        _characterController.OnPrimaryAttack -= PrimaryAttack;
        _characterController.OnSecondaryAttack -= SecondaryAttack;
    }
}
